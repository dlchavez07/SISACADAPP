using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISACADMovil.Services
{
    public static class SessionService
    {
        private const string ClaveUsuario = "ClaveUsuario";
        private const string ClaveCedula = "ClaveCedula";
        private const string ClaveNombreUsuario = "ClaveNombreUsuario";
        private const string ClaveRol = "ClaveRol";
        private const string ClaveCarrera = "ClaveCarrera";
        private const string ClaveCodCarrera = "ClaveCodCarrera";
        private const string ClavePeriodo = "ClavePeriodo";
        private const string ClaveFoto = "ClaveFoto";

        public static void SaveRol(string rol)
        {
            Preferences.Set(ClaveRol, rol);
        }

        public static string GetRol()
        {
            return Preferences.Get(ClaveRol, string.Empty);
        }

        public static void SaveFoto(string foto)
        {
            Preferences.Set(ClaveFoto, foto);
        }

        public static string GetFoto()
        {
            return Preferences.Get(ClaveFoto, string.Empty);
        }
        public static void SaveCodCarrera(string codCarrera)
        {
            Preferences.Set(ClaveCodCarrera, codCarrera);
        }

        public static string GetCodCarrera()
        {
            return Preferences.Get(ClaveCodCarrera, string.Empty);
        }

        public static void SavePeriodo(string periodo)
        {
            Preferences.Set(ClavePeriodo, periodo);
        }

        public static string GetPeriodo()
        {
            return Preferences.Get(ClavePeriodo, string.Empty);
        }

        public static void SaveCarrera(string carrera)
        {
            Preferences.Set(ClaveCarrera, carrera);
        }

        public static string GetCarrera()
        {
            return Preferences.Get(ClaveCarrera, string.Empty);
        }

        public static void SaveNombreUsuario(string nombreUsuario)
        {
            Preferences.Set(ClaveNombreUsuario, nombreUsuario);
        }

        public static string GetNombreUsuario()
        {
            return Preferences.Get(ClaveNombreUsuario, string.Empty);
        }

        public static void SaveUser(string nombreUsuario)
        {
            Preferences.Set(ClaveUsuario, nombreUsuario);
        }

        public static string GetUser()
        {
            return Preferences.Get(ClaveUsuario, string.Empty);
        }

        public static void SaveCedula(string cedula)
        {
            Preferences.Set(ClaveCedula, cedula);
        }

        public static string GetCedula()
        {
            return Preferences.Get(ClaveCedula, string.Empty);
        }

        public static void ClearSession()
        {
            Preferences.Remove(ClaveUsuario);
            Preferences.Remove(ClaveCedula);
            Preferences.Remove(ClaveNombreUsuario);
            Preferences.Remove(ClaveRol);
            Preferences.Remove(ClaveCarrera);
            Preferences.Remove(ClaveCodCarrera);
            Preferences.Remove(ClavePeriodo);
            Preferences.Remove(ClaveFoto);
        }
    }
}
