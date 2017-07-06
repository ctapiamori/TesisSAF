using System;
using System.Configuration;

namespace SOCAUD.Common.Constantes
{
    public class Config
    {
      
        public static string RutaArchivo
        {
            get { return ConfigurationManager.AppSettings["RutaArchivo"]; }
        }
        public static float MaxTamanioPorArchivo
        {
            get { return Convert.ToSingle(ConfigurationManager.AppSettings["MaxTamanioPorArchivo"]); }
        }

        public static string RutaByPassLogin
        {
            get { return ConfigurationManager.AppSettings["RutaByPassLogin"]; }
        }

        public static string RutaByPassLogout
        {
            get { return ConfigurationManager.AppSettings["RutaByPassLogout"]; }
        }

        public static string RutaRegisterNewSoaAuditor
        {
            get { return ConfigurationManager.AppSettings["RutaByRegistrarSOAAuditor"]; }
        }

        public static string RutaPantallaLogin
        {
            get { return ConfigurationManager.AppSettings["RutaGoToLogin"]; }
        }
        
    }
}
