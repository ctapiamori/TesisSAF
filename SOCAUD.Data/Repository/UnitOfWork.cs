using SOCAUD.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SOCAUD.Data.Repository
{
    #region Interface
    public interface IUnitOfWork : IDisposable
    {

        /// <summary>
        /// Call this to commit the unit of work
        /// </summary>
        void Commit();

        /// <summary>
        /// Return the database reference for this UOW
        /// </summary>
        DbContext Db { get; }

        /// <summary>
        /// Starts a transaction on this unit of work
        /// </summary>
        void StartTransaction();

        SOCAUDEntities DataContext();

    }
    #endregion

    #region Implementation
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory _databaseFactory;
        private TransactionScope _transaction;
        private readonly SOCAUDEntities _db;

        public UnitOfWork()
        {
            _db = new SOCAUDEntities();
            this._databaseFactory = new DatabaseFactory();

        }

        public void Dispose()
        {
        }

        public void StartTransaction()
        {
            _transaction = new TransactionScope();
        }

        public void Commit()
        {
            _db.SaveChanges();
            _transaction.Complete();
        }

        public DbContext Db
        {
            get { return _db; }
        }


        public SOCAUDEntities DataContext()
        {
            return _databaseFactory.Get();
        }
    }
    #endregion
}
