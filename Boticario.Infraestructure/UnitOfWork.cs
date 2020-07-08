using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using Boticario.Domain.Interfaces;

namespace Boticario.Infraestructure
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private DbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void BeginTransaction()
        {

            this._transaction = _context.Database.BeginTransaction();

        }

        public void BeginTransaction(System.Data.IsolationLevel transactionIsolationLevel)
        {
            this._transaction = _context.Database.BeginTransaction(transactionIsolationLevel);
        }

        public void Commit()
        {
            if (_context != null)
                _context.SaveChanges();

            if (this._transaction != null)
            {
                this._transaction.Commit();
                this._transaction.Dispose();
                this._transaction = null;
            }
        }

        public void RollBack()
        {
            if (this._transaction != null)
            {
                this._transaction.Rollback();
                this._transaction.Dispose();
                this._transaction = null;
            }

        }

        public void CommitAndRecreate()
        {
            Commit();
            this._transaction = _context.Database.BeginTransaction();
        }

        public void CommitAndRecreate(System.Data.IsolationLevel transactionIsolationLevel)
        {
            Commit();
            this._transaction = _context.Database.BeginTransaction(transactionIsolationLevel);
        }

        public void Dispose()
        {
            if (this._transaction != null)
            {
                this._transaction.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
