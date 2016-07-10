using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Common.Exceptions
{
    public class ExcepcionNegocio : Exception
    {
        public ExcepcionNegocio()
            : base() { }
        public ExcepcionNegocio(string message)
            : base(message) { }

        public ExcepcionNegocio(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public ExcepcionNegocio(string message, Exception innerException)
            : base(message, innerException) { }

        public ExcepcionNegocio(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected ExcepcionNegocio(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
