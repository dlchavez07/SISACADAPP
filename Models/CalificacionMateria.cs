using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISACADMovil.Models
{
    public class CalificacionMateria
    {
        public string Cod_materia { get; set; }
        public string Materia { get; set; }
        public string Pro_cedula { get; set; }
        public string Pro_nombre { get; set; }
        public int Nivel { get; set; }
        public double? Aporte1 { get; set; }
        public double? Aporte2 { get; set; }
        public double? Aporte3 { get; set; }
        public double? Nota_final { get; set; }
        public double? Supletorio { get; set; }
        public string Horario { get; set; }
        public string Estado { get; set; }
        public double? Asistencia { get; set; }
        public double? cuaderno { get; set; }
        public double? proyecto { get; set; }
        public string VersionCalificaciones { get; set; }
        public string Periodo_academico { get; set; }
        public string nombreAporte1 { get; set; }
        public string nombreAporte2 { get; set; }
        public string nombreAporte3 { get; set; }
        public string nombreAporte4 { get; set; }
        public string nombreAporte5 { get; set; }
        public bool ColumnAporte4 { get; set; }
        public bool ColumnAporte5 { get; set; }
        public string CodigoAulaMoodle { get; set; }
        public List<Actividad> Actividades { get; set; }
    }

    public class Actividad
    {
        public string tipoComponenteEvaluacion { get; set; }
        public string nota_final { get; set; }
        public string estado { get; set; }
        public string colorActividad { get; set; }
    }
}
