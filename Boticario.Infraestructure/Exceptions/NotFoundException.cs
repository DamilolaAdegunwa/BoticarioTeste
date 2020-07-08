using System;

namespace Boticario.Infraestructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
        public NotFoundException() : base("ResourceNotFound") { }
    }
}
