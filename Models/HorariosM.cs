using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISACADMovil.Models
{
    public class HorariosM
    {
        public string CodigoAulaMoodle { get; set; }
        public string Modulo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Docentes { get; set; }
        public string Materias { get; set; }
        public string Aula { get; set; }
        public string Color { get; set; }
        public string Jornadas { get; set; }
        public string Foto { get; set; }
        public string Teams { get; set; }
        public string Telegram { get; set; }
        public string correo { get; set; }
        public List<HorarioDia> HorarioDias { get; set; }

        public string FechaInicioFormatted
        {
            get
            {
                if (FechaInicio.Contains("0:00:00"))
                {
                    return FechaInicio.Replace("0:00:00", "").Trim();
                }

                return FechaInicio.Replace("0:00:00", "").Trim();

            }
        }

        public string FechaFinFormatted
        {
            get
            {
                if (FechaFin.Contains("0:00:00"))
                {
                    return FechaFin.Replace("0:00:00", "").Trim();
                }

                return FechaFin.Replace("0:00:00", "").Trim();
            }
        }
    }
    public class HorarioDia
    {
        public string Dia { get; set; }
        public List<string> Horas { get; set; }
        public string HorasString => string.Join(" ", Horas);
    }

}
