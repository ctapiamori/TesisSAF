using SOCAUD.Common.OperationalManagement;
using SOCAUD.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Repository
{
    #region Interface
    public interface IDatabaseFactory : IDisposable
    {
        SOCAUDEntities Get();
    }
    #endregion

    #region Implementation
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private SOCAUDEntities ctx;

        public SOCAUDEntities Get()
        {
            return ctx ?? (ctx = new SOCAUDEntities());
        }

        protected override void DisposeCore()
        {
            if (ctx != null)
                ctx.Dispose();
        }
    }
    #endregion
}
