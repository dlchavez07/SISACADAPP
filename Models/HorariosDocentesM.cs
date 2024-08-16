using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISACADMovil.Models
{
    public class HorariosDocentesM
    {
        public string CodigoAulaMoodle { get; set; }
        public string Modulo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Materias { get; set; }
        public string Aula { get; set; }
        public string Jornadas { get; set; }
        public ObservableCollection<CarreraMateria> MateriasList { get; set; }
        public List<HorariosxDia> HorarioDias { get; set; } // Propiedad correcta
    }

    public class HorariosxDia
    {
        public string Dia { get; set; }
        public List<string> Horas { get; set; }
        public string HorasString => string.Join(" / ", Horas);
    }

    public class CarreraMateria
    {
        public string Carrera { get; set; }
        public string Materia { get; set; }
    }
}
