using System;
using Boticario.Domain.Interfaces;

namespace Boticario.Service.Services
{
    public class BaseService : IDisposable
    {
        protected IUnitOfWorkFactory _unitOfWorkFactory { get; }
        private IUnitOfWork _unitOfWorkItem;
        private IUnitOfWork _unitOfWork
        {
            get
            {
                if (_unitOfWorkItem == null)
                    _unitOfWorkItem = _unitOfWorkFactory.Create();
                return _unitOfWorkItem;
            }
        }

        public BaseService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;

        }

        protected void BeginTransaction()
        {
            _unitOfWork.BeginTransaction();
        }

        protected void RollBack()
        {
            _unitOfWork.RollBack();
        }

        protected void Save()
        {
            _unitOfWork.Save();
        }
        protected void BeginTransaction(System.Data.IsolationLevel transactionIsolationLevel)
        {
            _unitOfWork.BeginTransaction(transactionIsolationLevel);
        }

        protected void CommitAndRecreate()
        {
            _unitOfWork.CommitAndRecreate();
        }

        protected void CommitAndRecreate(System.Data.IsolationLevel transactionIsolationLevel)
        {
            _unitOfWork.CommitAndRecreate(transactionIsolationLevel);
        }

        protected virtual void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            _unitOfWorkItem.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
