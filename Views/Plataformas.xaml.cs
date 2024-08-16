using SISACADMovil.Services;
using SISACADMovil.ViewModels;

namespace SISACADMovil.Views;

public partial class Plataformas : ContentPage
{
    HorariosViewModel _horariosViewModel;
    public Plataformas()
	{
		InitializeComponent();
        _horariosViewModel = new HorariosViewModel();
        this.BindingContext = _horariosViewModel;
        string cedula = SessionService.GetCedula();
        string periodo = SessionService.GetPeriodo();
        string codCarrera = SessionService.GetCodCarrera();
        _horariosViewModel.Cedula = cedula;
        _horariosViewModel.Periodo = periodo;
        _horariosViewModel.CodCarrea = codCarrera;
        this.Appearing += Horarios_Appearing;
    }
    private async void Horarios_Appearing(object sender, EventArgs e)
    {
        await _horariosViewModel.LoadSchedulesCommand.ExecuteAsync(null);
        await _horariosViewModel.LoadHorariosAdicionalesCommand.ExecuteAsync(null);
    }

    private async void OnTelegramImageButtonClicked(object sender, EventArgs e)
    {
        var imageButton = sender as ImageButton;
        var telegramUrl = imageButton.CommandParameter as string;

        if (!string.IsNullOrEmpty(telegramUrl))
        {
            await Launcher.OpenAsync(new Uri(telegramUrl));
        }
    }

    
}