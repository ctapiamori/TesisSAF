﻿using System;
using System.Configuration;

namespace SOCAUD.Common.Constantes
{
    public class Config
    {
        #region Web Config
        public static string RutaArchivo
        {
            get { return ConfigurationManager.AppSettings["RutaArchivo"]; }
        }
        public static float MaxTamanioPorArchivo
        {
            get { return Convert.ToSingle(ConfigurationManager.AppSettings["MaxTamanioPorArchivo"]); }
        }
        #endregion
    }
}