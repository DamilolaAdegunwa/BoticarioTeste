using System;
using System.Collections.Generic;
using System.Text;
using Boticario.Domain.Interfaces;

namespace Boticario.Infraestructure
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private BoticarioContext _context;
        public UnitOfWorkFactory(BoticarioContext context)
        {
            _context = context;
        }
        public IUnitOfWork Create()
        {
            return new UnitOfWork(_context);
        }
    }
}
