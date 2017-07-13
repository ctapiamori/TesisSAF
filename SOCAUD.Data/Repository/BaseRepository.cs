using SOCAUD.Common.Enum;
using SOCAUD.Common.OperationalManagement;
using SOCAUD.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SOCAUD.Common.Extensions;
using System.Linq.Dynamic;

namespace SOCAUD.Data.Repository
{
    #region Interface
    public interface IBaseRepository<T>
    {
        T GetById(object primaryKey);
        IEnumerable<T> GetAll();
        bool Exists(object primaryKey);
        T Add(T entity);
        T Update(T entity);
        void Delete(object id);
        T Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        IUnitOfWork UnitOfWork { get; }
        void UpdatePartial(dynamic entity, params Expression<Func<T, object>>[] property);
        void UpdatePartial(dynamic entity, string[] properties);
    }
    #endregion

    #region Implementation
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        internal DbSet<T> dbSet;
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;
            this.dbSet = _unitOfWork.Db.Set<T>();
        }

        public T GetById(object primaryKey)
        {

            var dbResult = dbSet.Find(primaryKey);
            return dbResult;

        }

        public bool Exists(object primaryKey)
        {
            return dbSet.Find(primaryKey) == null ? false : true;
        }

        public virtual T Add(T entity)
        {

            SetValue(ref entity, "FECREG", DateTime.Now);
            SetValue(ref entity, "USUREG", (System.Web.HttpContext.Current.Session["sessionUsuario"] == null) ? "" : System.Web.HttpContext.Current.Session["sessionUsuario"].ToString());
            SetValue(ref entity, "ESTREG", ((int)Estado.Auditoria.Activo).ToString());
            dynamic obj = dbSet.Add(entity);
            this._unitOfWork.Db.SaveChanges();
            return obj;
        }

        public virtual T Update(T entity)
        {
            SetValue(ref entity, "FECMOD", DateTime.Now);
            SetValue(ref entity, "USUMOD", (System.Web.HttpContext.Current.Session["sessionUsuario"] == null) ? "" : System.Web.HttpContext.Current.Session["sessionUsuario"].ToString());
            SetValue(ref entity, "ESTREG", ((int)Estado.Auditoria.Activo).ToString());
            dbSet.Attach(entity);
            _unitOfWork.Db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            this._unitOfWork.Db.SaveChanges();
            return entity;
        }
        public void Delete(object id)
        {
            T entity = dbSet.Find(id);
            SetValue(ref entity, "FECMOD", DateTime.Now);
            SetValue(ref entity, "USUMOD", (System.Web.HttpContext.Current.Session["sessionUsuario"] == null) ? "" : System.Web.HttpContext.Current.Session["sessionUsuario"].ToString());
            SetValue(ref entity, "ESTREG", ((int)Estado.Auditoria.Inactivo).ToString());
            dbSet.Attach(entity);
            _unitOfWork.Db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            this._unitOfWork.Db.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            Filter(ref query);
            return query.ToList();
            //return dbSet.AsEnumerable().ToList();
        }


        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = dbSet;
            Filter(ref query, filter);
            return orderBy != null ? orderBy(query).SingleOrDefault() : query.SingleOrDefault();
        }

        public IEnumerable<T> GetMany(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            Filter(ref query, filter);
            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        private static void Filter<T>(ref IQueryable<T> query, Expression<Func<T, bool>> filter = null)
        {

            var hasProperty = typeof(T).HasProperty("ESTREG");
            const string filterAdd = "ESTREG == \"1\"";
            if (filter != null)
            {
                query = hasProperty ? query.Where(filter).Where(filterAdd) : query.Where(filter);
            }
            else
            {
                if (hasProperty)
                {
                    query = query.Where(filterAdd);
                }
            }
        }

        private static void SetValue(ref T obj, string property, object value)
        {
            var propertyInfo = obj.GetType().GetProperty(property);
            var type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
            var safeValue = (value == null) ? null : Convert.ChangeType(value, type);
            propertyInfo.SetValue(obj, safeValue, null);
        }

        public IUnitOfWork UnitOfWork { get { return _unitOfWork; } }
        internal DbContext Database { get { return _unitOfWork.Db; } }



        public void UpdatePartial(dynamic entity, Expression<Func<T, object>>[] properties)
        {
            //entity.CAUD_USR_UPD = HttpContext.Current.Session["User"].ToString();
            //entity.DAUD_FEC_UPD = DateTime.Now;

            var entry = _unitOfWork.Db.Entry(entity);
            _unitOfWork.Db.Set<T>().Attach(entity);

            foreach (var item in properties)
            {
                entry.Property(item).IsModified = true;
            }

            //entry.Property("CAUD_USR_UPD").IsModified = true;
            //entry.Property("DAUD_FEC_UPD").IsModified = true;
        }

        public void UpdatePartial(dynamic entity, string[] properties)
        {
            //entity.CAUD_USR_UPD = HttpContext.Current.Session["User"].ToString();
            //entity.DAUD_FEC_UPD = DateTime.Now;

            var entry = _unitOfWork.Db.Entry(entity);
            _unitOfWork.Db.Set<T>().Attach(entity);
            foreach (var name in properties)
            {
                entry.Property(name).IsModified = true;
            }
            //entry.Property("CAUD_USR_UPD").IsModified = true;
            //entry.Property("DAUD_FEC_UPD").IsModified = true;
        }
    }
    #endregion
}
