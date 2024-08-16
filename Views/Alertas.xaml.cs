namespace SISACADMovil.Views;

public partial class Alertas : ContentPage
{
    public Alertas(string title, string message)
    {
        InitializeComponent();
        AlertTitle.Text = title;
        AlertMessage.Text = message;
    }

    private async void OnOkButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}