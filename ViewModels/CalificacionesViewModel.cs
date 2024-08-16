using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using SISACADMovil.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SISACADMovil.ViewModels
{
    public partial class CalificacionesViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        public CalificacionesViewModel()
        {
            _httpClient = new HttpClient();
            LoadCalificacionesCommand = new AsyncRelayCommand(LoadCalificacionesAsync);
        }

        public ObservableCollection<CalificacionMateria> Calificaciones { get; } = new ObservableCollection<CalificacionMateria>();

        [ObservableProperty]
        private bool _isBusy;

        public string Cedula { get; set; }
        public string Periodo { get; set; }
        public string CodCarrea { get; set; }
        public IAsyncRelayCommand LoadCalificacionesCommand { get; }

        public List<ChartEntry> EntriesChart;


        private async Task LoadCalificacionesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var url = $"https://api.itsqmet.edu.ec/Api/SISACAD?cedula={Cedula}&periodo={Periodo}&carrera={CodCarrea}";
                var calificaciones = await _httpClient.GetFromJsonAsync<List<CalificacionMateria>>(url);

                if (calificaciones != null && calificaciones.Any())
                {
                    Calificaciones.Clear();
                    foreach (var calificacion in calificaciones)
                    {
                        var actividadesUrl = $"https://api.itsqmet.edu.ec/Api/SISACAD?strEstudiante={Cedula}&codigoAulaMoodle={calificacion.CodigoAulaMoodle}";
                        var actividades = await _httpClient.GetFromJsonAsync<List<Actividad>>(actividadesUrl);

                        calificacion.Actividades = actividades ?? new List<Actividad>();
                        Calificaciones.Add(calificacion);
                    }

                    EntriesChart = new List<ChartEntry>();
                    foreach (var materia in Calificaciones)
                    {
                        if (double.TryParse(materia.Nota_final.ToString(), out double notaFinal))
                        {
                            EntriesChart.Add(new ChartEntry((float)notaFinal)
                            {
                                Label = materia.Materia,
                                ValueLabel = notaFinal.ToString(),
                                Color = SKColor.Parse(materia.VersionCalificaciones)
                            });
                        }
                        else
                        {
                            EntriesChart.Add(new ChartEntry(0)
                            {
                                Label = materia.Materia,
                                ValueLabel = "0",
                                Color = SKColor.Parse(materia.VersionCalificaciones)
                            });
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No hay calificaciones disponibles.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
