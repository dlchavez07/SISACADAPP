using Microsoft.Maui.Controls;
using SISACADMovil.Services;
using SISACADMovil.ViewModels;
namespace SISACADMovil.Views
{
    public partial class Login : ContentPage
    {

        public Login()
        {
            InitializeComponent();
            SessionService.ClearSession();
            var viewModel = BindingContext as MainViewModel;
            if (viewModel != null)
            {
                viewModel.Mensaje += OnLoginMessageChanged;
            }
         
        }
        private async void OnLoginMessageChanged(object sender, string e)
        {
            if (e == "TRUE")
            {
               Application.Current.MainPage = new AppShell();
            }
            else
            {
                var alertPage = new Alertas("Login", e);
                await Application.Current.MainPage.Navigation.PushModalAsync(alertPage);
            }
        }
    }
}