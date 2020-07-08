using System;
using System.Collections.Generic;
using System.Text;

namespace Boticario.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        void BeginTransaction();

        void BeginTransaction(System.Data.IsolationLevel transactionIsolationLevel);

        void Commit();

        void RollBack();

        void Save();

        void CommitAndRecreate();

        void CommitAndRecreate(System.Data.IsolationLevel transactionIsolationLevel);

        void Dispose();
    }
}
