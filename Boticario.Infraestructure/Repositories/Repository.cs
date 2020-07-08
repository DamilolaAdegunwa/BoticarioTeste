using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Boticario.Domain.Entities;
using Boticario.Domain.Interfaces.Repositories;
using Boticario.Infraestructure.Exceptions;

namespace Boticario.Infraestructure.Repositories
{
    public class Repository<T, Key> : IRepository<T, Key> where T : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public virtual IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            this._context.SaveChanges();
            return entities;
        }

        public async virtual Task<T> UpdateAsync(Key key, T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var actualEntity = await FindAsync(key);
            if (actualEntity == null) throw new NotFoundException();

            _context.Entry(actualEntity).CurrentValues.SetValues(entity);
            if (actualEntity is EntityBase)
            {
                foreach (var property in typeof(EntityBase).GetProperties())
                {
                    _context
                        .Entry(actualEntity)
                        .Property(property.Name)
                        .IsModified = false;
                }
            }
            this._context.SaveChanges();
            return actualEntity;
        }

        public virtual T Update(Key key, T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var actualEntity = Find(key);
            if (actualEntity == null) throw new NotFoundException();

            _context.Entry(actualEntity).CurrentValues.SetValues(entity);
            if (actualEntity is EntityBase)
            {
                foreach (var property in typeof(EntityBase).GetProperties())
                {
                    _context
                        .Entry(actualEntity)
                        .Property(property.Name)
                        .IsModified = false;
                }
            }
            this._context.SaveChanges();
            return actualEntity;
        }

        public virtual T Find(Key key)
        {
            var primaryKeys = PrimaryKeys(_context);

            var keyProperties = typeof(Key).GetProperties();
            IQueryable<T> entities = _context.Set<T>();
            var parameter = Expression.Parameter(typeof(T), "x");
            if (keyProperties.Count() == 0)
            {
                var lambda = (Expression<Func<T, bool>>)
                    Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, primaryKeys[0]),
                            Expression.Constant(key)),
                        parameter);
                entities = entities.Where(lambda);
            }
            else
            {
                var i = 0;
                foreach (var property in keyProperties)
                {
                    var primaryKey = primaryKeys.ToList().Where(k => k == property.Name).First();
                    var lambda = (Expression<Func<T, bool>>)
                        Expression.Lambda(
                            Expression.Equal(
                                Expression.Property(parameter, primaryKey),
                                Expression.Constant(property.GetValue(key))),
                            parameter);
                    entities = entities.Where(lambda);
                    i++;
                }
            }

            return entities.FirstOrDefault();
        }

        public async virtual Task<T> FindAsync(Key key)
        {
            var primaryKeys = PrimaryKeys(_context);

            var keyProperties = typeof(Key).GetProperties();
            IQueryable<T> entities = _context.Set<T>();
            var parameter = Expression.Parameter(typeof(T), "x");
            if (keyProperties.Count() == 0)
            {
                var lambda = (Expression<Func<T, bool>>)
                    Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, primaryKeys[0]),
                            Expression.Constant(key)),
                        parameter);
                entities = entities.Where(lambda);
            }
            else
            {
                var i = 0;
                foreach (var property in keyProperties)
                {
                    var primaryKey = primaryKeys.ToList().Where(k => k == property.Name).First();
                    var lambda = (Expression<Func<T, bool>>)
                        Expression.Lambda(
                            Expression.Equal(
                                Expression.Property(parameter, primaryKey),
                                Expression.Constant(property.GetValue(key))),
                            parameter);
                    entities = entities.Where(lambda);
                    i++;
                }
            }

            return await entities.FirstOrDefaultAsync();
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            this._context.SaveChanges();
        }

        

        public virtual T Delete(Key key)
        {
            var entity = Find(key);
            if (entity == null) throw new NotFoundException();
            Delete(entity);
            return entity;
        }

        public virtual void Dispose()
        {
            _context.Dispose();
        }

        public virtual T Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Add(entity);
            this._context.SaveChanges();
            return entity;
        }

        public int Count(Func<T, bool> include)
        {
            IEnumerable<T> entities = _context.Set<T>();
            if (include != null)
            {
                entities = entities.Where(include);
            }
            return entities.Count();
        }

        public virtual List<T> List(Func<T, bool> filter)
        {
            IEnumerable<T> entities = _context.Set<T>();

            if (filter != null)
            {
                entities = entities.Where(filter);
            }

            return entities.ToList();
        }

        private List<string> PrimaryKeys(DbContext context)
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var primaryKeys = entityType.FindPrimaryKey();

            return primaryKeys.Properties.Select(k => k.Name).ToList();
        }

        public virtual void Clean()
        {
            var set = _context.Set<T>();
            set.RemoveRange(set);
        }

        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Add(entity);
            await this._context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> ListAsync()
        {
            return await _context.Set<T>()
                .ToListAsync();
        }

        public async Task<T> DeleteAsync(Key key)
        {
            var entity = await FindAsync(key);
            if (entity == null) throw new NotFoundException();
            Delete(entity);
            return entity;
        }

        public async Task SaveAsync(Key key, T entity)
        {
            var result = await FindAsync(key);
            var entityAlreadyExists = result != null;
            if (entityAlreadyExists)
            {
                _context.Update(entity);
            }
            else
            {
                _context.Add(entity);
            }

            await _context.SaveChangesAsync();
        }
    }
}
