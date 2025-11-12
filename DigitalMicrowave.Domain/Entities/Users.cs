using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMicrowave.Domain.Entities
{
    public class Users
    {   
        public int Id { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
