using System;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using SISACADMovil.Models;
using SISACADMovil.Services;

namespace SISACADMovil.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _nombreUsuario;
        private string _contrasena;
        private bool _navegando;
        private bool _ocupado;

        public string NombreUsuario
        {
            get => _nombreUsuario;
            set
            {
                if (_nombreUsuario != value)
                {
                    _nombreUsuario = value;
                    OnPropertyChanged(nameof(NombreUsuario));
                }
            }
        }

        public string Contrasena
        {
            get => _contrasena;
            set
            {
                if (_contrasena != value)
                {
                    _contrasena = value;
                    OnPropertyChanged(nameof(Contrasena));
                }
            }
        }

        public bool Ocupado
        {
            get => _ocupado;
            set
            {
                if (_ocupado != value)
                {
                    _ocupado = value;
                    OnPropertyChanged(nameof(Ocupado));
                }
            }
        }

        public Command ComandoIniciarSesion { get; }

        public event EventHandler<string> Mensaje;

        public MainViewModel()
        {
            Ocupado = false; // Inicializar Ocupado en falso
            ComandoIniciarSesion = new Command(async () => await IniciarSesionAsync(), () => !_navegando && !Ocupado);
        }

        private async Task IniciarSesionAsync()
        {
            if (Ocupado)
                return;

            Ocupado = true;
            _navegando = true;
            ComandoIniciarSesion.ChangeCanExecute();

            string mensaje;
            try
            {
                if (!string.IsNullOrEmpty(NombreUsuario) && !string.IsNullOrEmpty(Contrasena))
                {
                    // Lógica de tu login
                    var clave = SeguridadM.EncriptarTexto(Contrasena);
                    string url = $"https://api.itsqmet.edu.ec/api/SISACAD?cod_usu={NombreUsuario}&pass={clave}";

                    using var cliente = new HttpClient();
                    var respuesta = await cliente.GetAsync(url);

                    if (respuesta.StatusCode == HttpStatusCode.OK)
                    {
                        string contenido = await respuesta.Content.ReadAsStringAsync();
                        var resultado = JsonConvert.DeserializeObject<List<MainModel>>(contenido);

                        if (resultado != null && resultado.Count > 0)
                        {
                            // Guardar información de sesión
                            SessionService.SaveCedula(resultado[0].Identificacion);
                            SessionService.SaveNombreUsuario(resultado[0].Nombre);
                            SessionService.SaveUser(NombreUsuario);
                            SessionService.SaveRol(Convert.ToString(resultado[0].Id_rol));

                            if (resultado[0].Id_rol == 3 || resultado[0].Id_rol == 4)
                            {
                                string url1 = $"https://api.itsqmet.edu.ec/api/SISACAD?identificacion={resultado[0].Identificacion}";
                                var respuesta1 = await cliente.GetAsync(url1);

                                if (respuesta1.StatusCode == HttpStatusCode.OK)
                                {
                                    string contenido1 = await respuesta1.Content.ReadAsStringAsync();
                                    var resultado1 = JsonConvert.DeserializeObject<List<EstudianteM>>(contenido1);

                                    if (resultado1 != null && resultado1.Count > 0)
                                    {
                                        SessionService.SaveCarrera(resultado1[0].Nombre_carrera);
                                        SessionService.SavePeriodo(resultado1[0].Periodo_academico);
                                        SessionService.SaveCodCarrera(resultado1[0].Codigo_carrera);
                                        SessionService.SaveFoto(resultado1[0].foto);

                                        mensaje = "TRUE";
                                        Mensaje?.Invoke(this, mensaje);
                                    }
                                    else
                                    {
                                        mensaje = "Estudiante no matriculado en un periodo vigente.";
                                        Mensaje?.Invoke(this, mensaje);
                                    }
                                }
                            }
                            else
                            {
                                SessionService.SaveFoto("si");
                                mensaje = "TRUE";
                                Mensaje?.Invoke(this, mensaje);
                            }
                        }
                        else
                        {
                            mensaje = "Estimado usuario debe ingresar las credenciales correctas";
                            Mensaje?.Invoke(this, mensaje);
                        }
                    }
                    else
                    {
                        mensaje = "Error en la respuesta del servidor.";
                        Mensaje?.Invoke(this, mensaje);
                    }
                }
                else
                {
                    mensaje = "Estimado usuario, campos vacíos.";
                    Mensaje?.Invoke(this, mensaje);
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error en el acceso. Contacte a soporte técnico: +593 98 464 1470.\nError: {ex.Message}";
                Mensaje?.Invoke(this, mensaje);
            }
            finally
            {
                _navegando = false;
                Ocupado = false;
                ComandoIniciarSesion.ChangeCanExecute();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
