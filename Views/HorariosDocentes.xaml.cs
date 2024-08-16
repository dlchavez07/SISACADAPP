using SISACADMovil.Services;
using SISACADMovil.ViewModels;

namespace SISACADMovil.Views
{
    public partial class HorariosDocentes : ContentPage
    {
        private HorariosDocentesViewModel _viewModel;

        public HorariosDocentes()
        {
            InitializeComponent();
            string cedula = SessionService.GetCedula();
            _viewModel = new HorariosDocentesViewModel();
            _viewModel.Cedula = cedula;
            BindingContext = _viewModel;

            // Suscribirse al evento SelectedIndexChanged del Picker
            PeriodosPicker.SelectedIndexChanged += PeriodosPicker_SelectedIndexChanged;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Cargar los periodos al iniciar
            if (_viewModel.LoadPeriodosCommand.CanExecute(null))
            {
                await _viewModel.LoadPeriodosCommand.ExecuteAsync(null);
            }
        }

        private async void PeriodosPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el índice seleccionado
            var picker = sender as Picker;
            if (picker == null) return;

            // Actualizar la propiedad PeriodoSeleccionado en el ViewModel
            _viewModel.PeriodoSeleccionado = picker.SelectedItem as Periodo;

            // Ejecutar el comando para cargar los horarios
            if (_viewModel.LoadSchedulesCommand.CanExecute(null))
            {
                await _viewModel.LoadSchedulesCommand.ExecuteAsync(null);
            }
        }
    }
}
