using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISACADMovil.Models
{
    public class EstudianteM
    {
        public string Cedula { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Codigo_carrera { get; set; }
        public string Nombre_carrera { get; set; }
        public string Periodo_academico { get; set; }
        public string FullCarrera { get; set; }
        public string foto { get; set; }
    }
    public class EstudianteDatosM
    {
        public byte[] Imagen { get; set; }
        public string Identificacion { get; set; }


    }
}
