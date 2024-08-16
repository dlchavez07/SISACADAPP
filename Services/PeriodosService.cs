using Newtonsoft.Json;
using SISACADMovil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SISACADMovil.Services
{
    internal class PeriodosService
    {
        public List<EstudianteM> estudiantes;

        public async Task<List<EstudianteM>> GetPeriodos(string identificacion)
        {
            string url = "https://api.itsqmet.edu.ec/Api/SISACAD?identificacion=" + identificacion;
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            request.Headers.Add("Accpet", "aplication/json");

            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                estudiantes = JsonConvert.DeserializeObject<List<EstudianteM>>(content);


            }
            return estudiantes;
        }
    }
}
