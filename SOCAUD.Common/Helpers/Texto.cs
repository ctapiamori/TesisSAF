using System.Globalization;
using System.IO;

namespace SOCAUD.Common.Helpers
{
    public class Texto
    {
        public static string LetraCapital(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return string.Empty;
            TextInfo textInfo = new CultureInfo("Es-Es", false).TextInfo;
            return textInfo.ToTitleCase(s.ToLower());
        }

        public static string NombreCompleto(string nombres, string apellidos)
        {
            var nomCompleto = string.Format("{0} {1}", LetraCapital(nombres), LetraCapital(apellidos));
            return nomCompleto.Trim();
        }

        public static string NombreCompleto(string nombres, string apePaterno, string apeMaterno)
        {
            var nomCompleto = string.Format("{0} {1} {2}", LetraCapital(nombres), LetraCapital(apePaterno), LetraCapital(apeMaterno));
            return nomCompleto.Trim();
        }

        public static string TipoMime(string archivo)
        {
            var contentType = "application/octetstream";
            var extension = Path.GetExtension(archivo).ToLower();
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension);
            if (key != null && key.GetValue("Content Type") != null)
            {
                contentType = key.GetValue("Content Type").ToString();
            }
            return contentType;
        }
    }
}
