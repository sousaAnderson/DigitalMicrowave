using DigitalMicrowave.Domain.Entities;
using DigitalMicrowave.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMicrowave.Application.Services
{
    public class UserServices
    {
        private readonly IUserRepository _repo;
        public UserServices(IUserRepository repo)
        {
            _repo = repo;
        }
        public Users GetUser(string user, string password)
        {
            return _repo.GetUser(user, password);
        }
    }
}
