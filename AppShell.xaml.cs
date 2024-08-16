using SISACADMovil.Services;

namespace SISACADMovil
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            MuestraMenu();
        }
        private async void OnLogoutClicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync("Login.xaml");
        }

        private void MuestraMenu()
        {
            string rol = SessionService.GetRol();

            if (rol == "3" || rol == "4")
            {
                HorariosDocentesItem.IsVisible = false;
                MenuPrincipalItem.IsVisible = true;
                CalificacionesItem.IsVisible = true;
                ConoceDocentesItem.IsVisible = true;
                HorariosItem.IsVisible = true;
                TutoriasItem.IsVisible = true;
                SalirItem.IsVisible = true;
            }
            else
            {
                HorariosDocentesItem.IsVisible = true;
                MenuPrincipalItem.IsVisible = true;
                CalificacionesItem.IsVisible = false;
                ConoceDocentesItem.IsVisible = false;
                HorariosItem.IsVisible = false;
                TutoriasItem.IsVisible = false;
                SalirItem.IsVisible = true;
            }


        }

    }
}
