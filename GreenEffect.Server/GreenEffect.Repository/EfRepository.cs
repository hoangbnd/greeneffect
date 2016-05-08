using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Transactions;
using MVCCore;
using MVCCore.Data;
using EntityFramework.BulkInsert.Extensions;
namespace GreenEffect.Repository
{
    /// <summary>
    /// Entity Framework repository
    /// </summary>
    public partial class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository(IDbContext context)
        {
            this._context = context;
        }

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public int Count()
        {
            return Entities.Count();
        }

        public int Count(List<Expression<Func<T, bool>>> expressions)
        {
            return expressions.Aggregate(Entities.AsQueryable(), (current, expression) => current.Where(expression)).Count();
        }

        public int Count(List<Expression<Func<T, bool>>> expressions, string whereClause)
        {
            return expressions.Aggregate(Entities.AsQueryable(), (current, expression) => current.Where(expression).Where(whereClause)).Count();
        }

        public T FindById(int id)
        {
            return Entities.FirstOrDefault(entity => entity.Id == id);
        }

        public T FindById(long id)
        {
            return Entities.FirstOrDefault(entity => entity.Id == id);
        }

        public T Find(List<Expression<Func<T, bool>>> expressions)
        {
            return expressions.Aggregate(Entities.AsQueryable(),
                                  (current, expression) => current.Where(expression)).FirstOrDefault();
        }

        public ICollection<T> FindAll()
        {
            return Entities.ToList();
        }

        public ICollection<T> FindAll(List<int> list)
        {
            return list.Select(i => Entities.SingleOrDefault(entity => entity.Id == i)).ToList();
        }

        public ICollection<T> FindAll(List<long> list)
        {
            return list.Select(i => Entities.SingleOrDefault(entity => entity.Id == i)).ToList();
        }

        public ICollection<T> FindAll(string order)
        {
            return string.IsNullOrEmpty(order)
                ? Entities.ToList()
                : Entities.OrderBy(order).ToList();
        }

        public ICollection<T> FindAll(List<Expression<Func<T, bool>>> expressions)
        {
            return expressions.Aggregate(Entities.AsQueryable(),
                                  (current, expression) => current.Where(expression)).ToList();
        }

        public ICollection<T> FindAll(List<Expression<Func<T, bool>>> expressions, int limit)
        {
            return expressions.Aggregate(Entities.AsQueryable(), (current, expression) => current.Where(expression)).Take(limit).ToList();
        }

        public ICollection<T> FindAll(List<Expression<Func<T, bool>>> expressions, string order)
        {
            return !string.IsNullOrEmpty(order)
                ? expressions.Aggregate(Entities.AsQueryable(),
                    (current, expression) => current.Where(expression).OrderBy(order)).ToList()
                : expressions.Aggregate(Entities.AsQueryable(),
                    (current, expression) => current.Where(expression)).ToList();
        }

        public ICollection<T> FindAll(List<Expression<Func<T, bool>>> expressions, int limit, string order)
        {
            return !string.IsNullOrEmpty(order)
                ? expressions.Aggregate(Entities.AsQueryable(),
                    (current, expression) => current.Where(expression)).OrderBy(order).Take(limit).ToList()
                : expressions.Aggregate(Entities.AsQueryable(),
                    (current, expression) => current.Where(expression)).Take(limit).ToList();
        }

        public ICollection<T> Random(List<Expression<Func<T, bool>>> expressions, int limit, string order)
        {
            if (expressions.Count > 0)
            {
                return expressions.Aggregate(Entities.AsQueryable(),
                                             (current, expression) => (IQueryable<T>)current.Where(expression).OrderBy(order).OrderBy(t => Guid.NewGuid())).Take(limit).ToList();
            }
            return Entities.OrderBy(order).OrderBy(t => Guid.NewGuid()).Take(limit).ToList();
        }

        public PagedList<T> Paging(string order, int pageIndex, int pageSize)
        {
            var start = (pageIndex - 1) * pageSize;
            var total = Count();
            var result = Entities.OrderBy(order).Skip(start).Take(pageSize).ToList();
            return new PagedList<T>(result, pageIndex, pageSize, total);
        }

        public PagedList<T> Paging(List<Expression<Func<T, bool>>> expressions, string order, int pageIndex, int pageSize)
        {
            var start = (pageIndex - 1) * pageSize;
            if (expressions.Count > 0)
            {
                var total = Count(expressions);

                ICollection<T> result;
                if (string.IsNullOrEmpty(order))
                {
                    result = expressions.Aggregate(Entities.AsQueryable(),
                                          (current, expression) => current.Where(expression)).Skip(start).Take(pageSize).ToList();
                }
                else
                {
                    result = expressions.Aggregate(Entities.AsQueryable(),
                                          (current, expression) => current.Where(expression)).OrderBy(order).Skip(start).Take(pageSize).ToList();
                }
                return new PagedList<T>(result, pageIndex, pageSize, total);
            }
            return Paging(order, pageIndex, pageSize);
        }

        public PagedList<T> Paging(List<Expression<Func<T, bool>>> expressions, string whereClause, string order, int pageIndex, int pageSize)
        {
            var start = (pageIndex - 1) * pageSize;
            if (expressions.Count > 0)
            {
                var total = Count(expressions, whereClause);

                ICollection<T> result;
                if (string.IsNullOrEmpty(order))
                {
                    result = expressions.Aggregate(Entities.AsQueryable(),
                                          (current, expression) => current.Where(expression).Where(whereClause)).Skip(start).Take(pageSize).ToList();
                }
                else
                {
                    result = expressions.Aggregate(Entities.AsQueryable(),
                                          (current, expression) => current.Where(expression).Where(whereClause)).OrderBy(order).Skip(start).Take(pageSize).ToList();
                }
                return new PagedList<T>(result, pageIndex, pageSize, total);
            }
            return Paging(order, pageIndex, pageSize);
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Add(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        public void Insert(ICollection<T> entities)
        {
            try
            {
                var dataSettingsManager = new DataSettingsManager();
                var dataProviderSettings = dataSettingsManager.LoadSettings();
                var context = new DbObjectContext(dataProviderSettings.DataConnectionString);
                
                context.BulkInsert(entities);

                context.SaveChanges();

                //foreach (var entity in entities)
                //{
                //    Entities.Add(entity);
                //}
                //var entitiesDb = entities.ToArray();
                //Entities.AddOrUpdate(entitiesDb);
                //_context.SaveChanges();

            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Remove(entity);

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        private IDbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }
    }
}