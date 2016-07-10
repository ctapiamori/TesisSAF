using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOCAUD.Data.Model;

namespace SOCAUD.Web.Helper
{
    public class ArchivoWeb
    {
        public string NOMBLABEL { get; set; }
        public string ARCNOMBFISICO { get; set; }
        public byte[] fileBytes { get; set; }
    }
}