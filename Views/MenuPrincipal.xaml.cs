using Microsoft.Maui.Controls;
using SISACADMovil.Services;
using SISACADMovil.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SISACADMovil.Views
{
    public partial class MenuPrincipal : ContentPage
    {
        private readonly HttpClient _httpClient;
        byte[] imgbit;
       
        public MenuPrincipal()
        {
            InitializeComponent();
            BindingContext = new MenuPrincipalViewModel();
            string rol = SessionService.GetRol();
            string foto = SessionService.GetFoto();

            

            if (rol == "3" || rol == "4")
            {
                docente.IsVisible = false;
                carrera.IsVisible = true;
                periodo.IsVisible = true;
                imgPerfil.Source = foto == "si" ? "https://sisacad.itsqmet.edu.ec:8070/FotosEstudiantes/" + Services.SessionService.GetCedula() + ".png" : "https://cdn.icon-icons.com/icons2/1747/PNG/512/profile_113589.png";
            }
            else
            {
                docente.IsVisible = true;
                carrera.IsVisible = false;
                periodo.IsVisible = false;
                imgPerfil.Source = foto == "si" ? "https://sisacad.itsqmet.edu.ec:8070/FotosDocentes/" + Services.SessionService.GetCedula() + ".png": "https://cdn.icon-icons.com/icons2/1747/PNG/512/profile_113589.png";
            }

            _httpClient = new HttpClient();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            string url = "https://sisacad.itsqmet.edu.ec/ITSQMETCarnet.aspx?FHFXOD="+SessionService.GetCedula();

            await GenerateQRCodeAsync(url);
        }

        private async Task GenerateQRCodeAsync(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                try
                {
                    // URL de la API para generar QR
                    string url = $"https://api.qrserver.com/v1/create-qr-code/?data={data}&size=90x90";

                    HttpResponseMessage response = await _httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var stream = await response.Content.ReadAsStreamAsync();

                    qrImage.Source = ImageSource.FromStream(() => stream);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"No se pudo generar el código QR: {ex.Message}", "OK");
                }
            }
            else
            {

                await DisplayAlert("Error", "No se pudo obtener la cédula", "OK");
            }
        }

        private async void OnUploadPhotoButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();

                if (result != null)
                {

                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, result.FileName);

                    using (Stream source = await result.OpenReadAsync())
                    using (FileStream localFile = File.OpenWrite(localFilePath))
                    {
                        await source.CopyToAsync(localFile);
                    }

                    await Task.Delay(500); 

                    using (FileStream fileStream = File.OpenRead(localFilePath))
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await fileStream.CopyToAsync(memoryStream);
                        imgbit = memoryStream.ToArray();
                        MenuPrincipalViewModel menu = new MenuPrincipalViewModel();
                        bool respuesta = await menu.GuardarImagen(imgbit, Services.SessionService.GetCedula());


                        if (respuesta)
                        {
                            imgPerfil.Source = new UriImageSource
                            {
                                Uri = new Uri(menu.ImagenCargada),
                                CachingEnabled = false
                            };

                            await DisplayAlert("Foto Subida", $"Imagen Guardada.", "OK");

                        }
                        else
                        {
                            var alertPage = new Alertas("Error", "No se pudo subir la foto");
                            await Application.Current.MainPage.Navigation.PushModalAsync(alertPage);
                            //await DisplayAlert("Error", $"No se pudo subir la foto", "OK");
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo subir la foto: {ex.Message}", "OK");
            }

        }
    }
}
