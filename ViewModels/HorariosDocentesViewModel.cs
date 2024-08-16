using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using SISACADMovil.Models;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace SISACADMovil.ViewModels
{
    public partial class HorariosDocentesViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        public HorariosDocentesViewModel()
        {
            _httpClient = new HttpClient();
            LoadSchedulesCommand = new AsyncRelayCommand(LoadSchedulesAsync);
            LoadPeriodosCommand = new AsyncRelayCommand(LoadPeriodosAsync);
            Periodos = new ObservableCollection<Periodo>();
            MatutinoSchedules = new ObservableCollection<HorariosDocentesM>();
            VespertinoSchedules = new ObservableCollection<HorariosDocentesM>();
            IntensivoSchedules = new ObservableCollection<HorariosDocentesM>();
            OnlineSchedules = new ObservableCollection<HorariosDocentesM>();

            // Cargar los periodos al iniciar
            LoadPeriodosAsync();
        }

        public ObservableCollection<HorariosDocentesM> MatutinoSchedules { get; } = new ObservableCollection<HorariosDocentesM>();
        public ObservableCollection<HorariosDocentesM> VespertinoSchedules { get; } = new ObservableCollection<HorariosDocentesM>();
        public ObservableCollection<HorariosDocentesM> IntensivoSchedules { get; } = new ObservableCollection<HorariosDocentesM>();
        public ObservableCollection<HorariosDocentesM> OnlineSchedules { get; } = new ObservableCollection<HorariosDocentesM>();

        [ObservableProperty]
        private bool _isBusy;
        public string Cedula { get; set; }

        public ObservableCollection<Periodo> Periodos { get; } = new ObservableCollection<Periodo>();

        [ObservableProperty]
        private Periodo _periodoSeleccionado;

        public IAsyncRelayCommand LoadSchedulesCommand { get; }
        public IAsyncRelayCommand LoadPeriodosCommand { get; }

        private async Task LoadSchedulesAsync()
        {
            if (IsBusy || PeriodoSeleccionado == null)
                return;

            try
            {
                IsBusy = true;

                // Utilizar el periodo seleccionado
                var periodo = PeriodoSeleccionado.Periodo_Nombre;

                // Cargar los horarios por cada tipo
                await LoadScheduleByTypeAsync("MATUTINO", MatutinoSchedules, periodo);
                await LoadScheduleByTypeAsync("VESPERTINO", VespertinoSchedules, periodo);
                await LoadScheduleByTypeAsync("INTENSIVO", IntensivoSchedules, periodo);
                await LoadScheduleByTypeAsync("EN LINEA", OnlineSchedules, periodo);
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

        private async Task LoadScheduleByTypeAsync(string tipo, ObservableCollection<HorariosDocentesM> targetCollection, string periodo)
        {
            var url = $"https://api.itsqmet.edu.ec/Api/SISACAD?strDocente={Cedula}&strPeriodo={periodo}&strTipo={tipo}";
            var schedules = await _httpClient.GetFromJsonAsync<List<HorariosDocentesM>>(url);

            if (schedules != null)
            {
                targetCollection.Clear();
                foreach (var schedule in schedules)
                {
                    schedule.HorarioDias = ParseJornadas(schedule.Jornadas);
                    schedule.MateriasList = ParseMaterias(schedule.Materias);
                    targetCollection.Add(schedule);
                }
            }
        }

        private async Task LoadPeriodosAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var url = "https://api.itsqmet.edu.ec/Api/SISACAD?periodoVigente=periodo";
                var response = await _httpClient.GetStringAsync(url);

                var periodos = JsonConvert.DeserializeObject<List<Periodo>>(response);

                if (periodos != null)
                {
                    Periodos.Clear();
                    foreach (var periodo in periodos)
                    {
                        Periodos.Add(periodo);
                    }
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

        private ObservableCollection<CarreraMateria> ParseMaterias(string materias)
        {
            var materiaList = new ObservableCollection<CarreraMateria>();
            var items = materias.Split('|', StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in items)
            {
                var parts = item.Split('/');
                if (parts.Length == 2)
                {
                    materiaList.Add(new CarreraMateria
                    {
                        Carrera = parts[0].Trim(),
                        Materia = parts[1].Trim()
                    });
                }
            }
            return materiaList;
        }
        private List<HorariosxDia> ParseJornadas(string jornadas)
        {
            var horarioDias = new List<HorariosxDia>();
            var dias = jornadas.Split('|', StringSplitOptions.RemoveEmptyEntries);
            foreach (var dia in dias)
            {
                var parts = dia.Split('/');
                if (parts.Length == 2)
                {
                    var horas = parts[1].Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var horarioDia = new HorariosxDia
                    {
                        Dia = parts[0].Trim(),
                        Horas = horas
                    };
                    horarioDias.Add(horarioDia);
                }
            }
            return horarioDias;
        }
    }

    public class Periodo
    {
        public string Periodo_Nombre { get; set; }
    }

}
