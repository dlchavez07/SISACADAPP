using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SISACADMovil.Models;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace SISACADMovil.ViewModels
{
    public partial class HorariosViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private int _currentIndex;
        private int _additionalCurrentIndex;

        public HorariosViewModel()
        {
            _httpClient = new HttpClient();
            LoadSchedulesCommand = new AsyncRelayCommand(LoadSchedulesAsync);
            LoadHorariosAdicionalesCommand = new AsyncRelayCommand(LoadHorariosAdicionalesAsync);

            PreviousCommand = new RelayCommand(GoToPrevious);
            NextCommand = new RelayCommand(GoToNext);
        }

        public ObservableCollection<HorariosM> Schedules { get; } = new ObservableCollection<HorariosM>();
        public ObservableCollection<HorariosM> AdditionalSchedules { get; } = new ObservableCollection<HorariosM>();

        [ObservableProperty]
        private bool _isBusy;

        public string Cedula { get; set; }
        public string Periodo { get; set; }
        public string CodCarrea { get; set; }

        public IAsyncRelayCommand LoadSchedulesCommand { get; }
        public IAsyncRelayCommand LoadHorariosAdicionalesCommand { get; }
        public RelayCommand PreviousCommand { get; }
        public RelayCommand NextCommand { get; }

        private void GoToPrevious()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                OnPropertyChanged(nameof(CurrentIndex));
            }
        }

        private void GoToNext()
        {
            if (_currentIndex < Schedules.Count - 1)
            {
                _currentIndex++;
                OnPropertyChanged(nameof(CurrentIndex));
            }
        }

        public int CurrentIndex
        {
            get => _currentIndex;
            set => SetProperty(ref _currentIndex, value);
        }

        private async Task LoadSchedulesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var url = $"https://api.itsqmet.edu.ec/Api/SISACAD?strEstudiante={Cedula}&periodo={Periodo}&carrera={CodCarrea}&tipoHorario=CARRERA";
                var schedules = await _httpClient.GetFromJsonAsync<List<HorariosM>>(url);

                if (schedules != null)
                {
                    Schedules.Clear();
                    foreach (var schedule in schedules)
                    {
                        schedule.HorarioDias = ParseJornadas(schedule.Jornadas);
                        Schedules.Add(schedule);
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

        private async Task LoadHorariosAdicionalesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var url = $"https://api.itsqmet.edu.ec/Api/SISACAD?strEstudiante={Cedula}&periodo={Periodo}&carrera={CodCarrea}&tipoHorario=ADICIONALES";
                var schedules = await _httpClient.GetFromJsonAsync<List<HorariosM>>(url);

                if (schedules != null)
                {
                    AdditionalSchedules.Clear();
                    foreach (var schedule in schedules)
                    {
                        schedule.HorarioDias = ParseJornadas(schedule.Jornadas);
                        AdditionalSchedules.Add(schedule);
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

        private List<HorarioDia> ParseJornadas(string jornadas)
        {
            var horarioDias = new List<HorarioDia>();
            var dias = jornadas.Split('|', StringSplitOptions.RemoveEmptyEntries);
            foreach (var dia in dias)
            {
                var parts = dia.Split('/');
                if (parts.Length == 2)
                {
                    var horas = parts[1].Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var horarioDia = new HorarioDia
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
}
