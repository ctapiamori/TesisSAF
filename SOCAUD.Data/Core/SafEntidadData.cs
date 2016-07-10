﻿using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafEntidadData : IBaseRepository<SAF_ENTIDADES> { }

    public class SafEntidadData : BaseRepository<SAF_ENTIDADES>, ISafEntidadData
    {
        public SafEntidadData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
