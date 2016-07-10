using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOCAUD.Web.Helper
{
    public class Gantt
    {

        public string name { get; set; }
        public string desc { get; set; }
        public List<GanttDet> values { get; set; }

        public Gantt()
        {
            this.values = new List<GanttDet>();
        }
    }

    public class GanttDet
    {

        public string from { get; set; }
        public string to { get; set; }
        public string label { get; set; }
        public string customClass { get; set; }
    }
}