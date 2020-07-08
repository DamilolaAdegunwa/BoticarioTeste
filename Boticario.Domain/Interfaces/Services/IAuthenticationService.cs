using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boticario.Domain.Interfaces.Services
{
    public interface IAuthenticationService
    {
        bool ValidateLogin(string email, string senha);
    }
}
