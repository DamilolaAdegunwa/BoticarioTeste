using System;
using System.Collections.Generic;
using System.Text;

namespace Boticario.Domain.Interfaces
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
