using Microcharts;
using SISACADMovil.Services;
using SISACADMovil.ViewModels;
using SkiaSharp;

namespace SISACADMovil.Views;

public partial class Calificaciones : ContentPage
{
    private CalificacionesViewModel _viewModel;
    public Calificaciones()
    {
        InitializeComponent();
      
        _viewModel = new CalificacionesViewModel();
        BindingContext = _viewModel;
        string cedula = SessionService.GetCedula();
        string periodo = SessionService.GetPeriodo();
        string codCarrera = SessionService.GetCodCarrera();
        _viewModel.Cedula = cedula;
        _viewModel.Periodo = periodo;
        _viewModel.CodCarrea = codCarrera;

  


    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel.LoadCalificacionesCommand.CanExecute(null))
        {
            await _viewModel.LoadCalificacionesCommand.ExecuteAsync(null);

            var chart = new BarChart { Entries = _viewModel.EntriesChart,
             LabelTextSize = 40,
            };
            chName.Chart = chart;
        }
    }
}