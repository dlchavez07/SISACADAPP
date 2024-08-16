
using Newtonsoft.Json;
using SISACADMovil.Models;
using SISACADMovil.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SISACADMovil.ViewModels
{
    public class MenuPrincipalViewModel : INotifyPropertyChanged
    {
        private string _nombre;
        private string _carrera;
        private string _rol;
        private string _periodo;
        private string _imagenCargada;

        public string Nombre
        {
            get => _nombre;
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ImagenCargada
        {
            get { return _imagenCargada; }
            set
            {
                if (_imagenCargada != value)
                {
                    _imagenCargada = value;
                    OnPropertyChanged(nameof(ImagenCargada));
                }
            }
        }
        public string Rol
        {
            get => _rol;
            set
            {
                if (_rol != value)
                {
                    _rol = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Carrera
        {
            get => _carrera;
            set
            {
                if (_carrera != value)
                {
                    _carrera = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Periodo
        {
            get => _periodo;
            set
            {
                if (_periodo != value)
                {
                    _periodo = value;
                    OnPropertyChanged();
                }
            }
        }

        public MenuPrincipalViewModel()
        {
            // Obtener el nombre de usuario de la sesión
            Nombre = SessionService.GetNombreUsuario();
            Rol = SessionService.GetRol();
            Carrera = SessionService.GetCarrera();
            Periodo = SessionService.GetPeriodo();
        }

        public async  Task<bool> GuardarImagen(byte[] imagen,string identificacionReg)
        {
            EstudianteDatosM estudianteDatos = new EstudianteDatosM();  
            bool respuesta = false;
            estudianteDatos.Imagen = imagen;
            estudianteDatos.Identificacion = identificacionReg;

            string url = "https://api.itsqmet.edu.ec/api/SISACAD/postimage";

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post
            };
            request.Headers.Add("Accpet", "application/json");
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(estudianteDatos);
            var contectJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(request.RequestUri, contectJson);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rol = SessionService.GetRol();
                if (rol == "3" || rol == "4")
                {
                    ImagenCargada = "https://sisacad.itsqmet.edu.ec:8070/FotosEstudiantes/" + Services.SessionService.GetCedula() + ".png";
                }
                else
                {
                    ImagenCargada = "https://sisacad.itsqmet.edu.ec:8070/FotosEstudiantes/" + Services.SessionService.GetCedula() + ".png";
                }
                respuesta = true;
            }
            return respuesta;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void OnpropertyChanged([CallerMemberName] string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

    }
}
