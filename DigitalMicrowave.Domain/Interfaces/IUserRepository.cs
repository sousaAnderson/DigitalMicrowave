using DigitalMicrowave.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMicrowave.Domain.Interfaces
{
    public interface IUserRepository
    {
        Users GetUser(string user, string password);
    }
}
