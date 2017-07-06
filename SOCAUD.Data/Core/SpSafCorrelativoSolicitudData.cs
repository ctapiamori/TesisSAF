using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SOCAUD.Data.Core
{

    public interface ISpSafCorrelativoSolicitudData : IBaseRepository<string>
    {
        string GenerarCorrelativo();
    }
    public class SpSafCorrelativoSolicitudData : BaseRepository<string>, ISpSafCorrelativoSolicitudData
    {
        private readonly IUnitOfWork _uow;

        public SpSafCorrelativoSolicitudData(IDatabaseFactory databaseFactory, IUnitOfWork uow)
            : base(uow)
        {
            _uow = uow;
        }

        public string GenerarCorrelativo()
        {
            try
            {
                return this._uow.DataContext().SP_SAF_CORRELATIVOSOLICITUD1().ToList().FirstOrDefault().NUMSOL.ToString();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
    }
}
