namespace SISACADMovil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           MainPage = new NavigationPage(new SISACADMovil.Views.Login());
           // MainPage = new AppShell();
        }
    }
}
