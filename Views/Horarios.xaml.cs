using SISACADMovil.Services;
using SISACADMovil.ViewModels;

namespace SISACADMovil.Views;

public partial class Horarios : ContentPage
{
    PeriodosViewModel mp;
    string periodo;
    HorariosViewModel _horariosViewModel;

    public Horarios()
	{
		InitializeComponent();
        _horariosViewModel = (HorariosViewModel)BindingContext;
        string cedula = SessionService.GetCedula();
        string periodo = SessionService.GetPeriodo();
        string codCarrera = SessionService.GetCodCarrera();
        _horariosViewModel.Cedula = cedula;
        _horariosViewModel.Periodo = periodo;
        _horariosViewModel.CodCarrea = codCarrera;
        this.BindingContext = _horariosViewModel;
        this.Appearing += Horarios_Appearing;
        //string cedula = SessionService.GetCedula();
        //mp = new PeriodosViewModel(cedula);
        //BindingContext = mp;
        //this.Appearing += Horarios_Appearing;

    }

    private async void Horarios_Appearing(object sender, EventArgs e)
    {
        await _horariosViewModel.LoadSchedulesCommand.ExecuteAsync(null);
        await _horariosViewModel.LoadHorariosAdicionalesCommand.ExecuteAsync(null);
    }
    

    //private async void Horarios_Appearing(object sender, EventArgs e)
    //{
    //    if (PeriodoPicker.ItemsSource == null)
    //    {
    //        await mp.MostrarPeriodos();
    //    }

    //    PeriodoPicker.SelectedIndex = 1;
    //    periodo = PeriodoPicker.Items[PeriodoPicker.SelectedIndex].ToString();

    //}
}