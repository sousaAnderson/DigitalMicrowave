using DigitalMicrowave.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMicrowave.Domain.Interfaces
{
    public interface IHeatingProgramRepository
    {
        IEnumerable<HeatingProgram> GetAll();
        HeatingProgram GetById(int id);
        bool CharacterExists(string character, int? ignoreId = null);
        void Create(HeatingProgram program);
        void Update(HeatingProgram program);
        void Delete(int id);
    }
}
