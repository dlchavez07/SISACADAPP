using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using SISACADMovil.Models;
using SISACADMovil.Services;

namespace SISACADMovil.ViewModels
{
    public class TutoriasViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TutoriasM> _tutorias;
        private bool _esCargando;

        public ObservableCollection<TutoriasM> Tutorias
        {
            get => _tutorias;
            set
            {
                if (_tutorias != value)
                {
                    _tutorias = value;
                    OnPropertyChanged(nameof(Tutorias));
                }
            }
        }

        public bool EsCargando
        {
            get => _esCargando;
            set
            {
                if (_esCargando != value)
                {
                    _esCargando = value;
                    OnPropertyChanged(nameof(EsCargando));
                }
            }
        }

        public ICommand CargarTutoriasCommand { get; }
        public TutoriasViewModel()
        {
            CargarTutoriasCommand = new Command(async () => await CargarTutoriasAsync());
            Tutorias = new ObservableCollection<TutoriasM>();
        }

        private async Task CargarTutoriasAsync()
        {
            if (EsCargando)
                return;

            EsCargando = true;

            try
            {
                string identificacionEstudiante = SessionService.GetCedula();
                string periodoAcademico = SessionService.GetPeriodo(); 

                if (!string.IsNullOrEmpty(identificacionEstudiante) && !string.IsNullOrEmpty(periodoAcademico))
                {
                    string url = $"https://api.itsqmet.edu.ec/Api/SISACAD?strEstudiante={identificacionEstudiante}&strPeriodo={periodoAcademico}";

                    using var cliente = new HttpClient();
                    var respuesta = await cliente.GetAsync(url);

                    if (respuesta.StatusCode == HttpStatusCode.OK)
                    {
                        string contenido = await respuesta.Content.ReadAsStringAsync();
                        var resultados = JsonConvert.DeserializeObject<List<TutoriasM>>(contenido);

                        if (resultados != null)
                        {
                            Tutorias.Clear();
                            foreach (var tutoria in resultados)
                            {
                                Tutorias.Add(tutoria);
                            }
                        }
                        else
                        {
                            // Manejo de casos cuando no hay datos
                            Tutorias.Clear();
                        }
                    }
                    else
                    {
                        // Manejo de errores de servidor
                        Tutorias.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine($"Error al cargar tutorías: {ex.Message}");
            }
            finally
            {
                EsCargando = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
