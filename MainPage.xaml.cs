namespace SISACADMovil
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            // Implementar la lógica de autenticación aquí
            if (username == "usuario" && password == "contraseña")
            {
                // Navegar a la página principal de la aplicación
                //Application.Current.MainPage = new MainPage();
            }
            else
            {
                // Mostrar mensaje de error
                await DisplayAlert("Error", "Usuario o contraseña incorrectos. Por favor, inténtalo de nuevo.", "OK");
                // errorMessage.Text = "Nombre de usuario o contraseña incorrectos";
                errorMessage.IsVisible = true;
            }
        }
    }

}
