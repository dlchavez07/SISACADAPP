using SISACADMovil.Services;
using SISACADMovil.ViewModels;

namespace SISACADMovil.Views;

public partial class Tutorias : ContentPage
{
    public Tutorias()
    {
        InitializeComponent();
        var viewModel = BindingContext as TutoriasViewModel;
        if (viewModel != null)
        {
            viewModel.CargarTutoriasCommand.Execute(null); 
        }
    }
}