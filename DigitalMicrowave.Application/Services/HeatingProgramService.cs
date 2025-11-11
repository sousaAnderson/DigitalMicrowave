using DigitalMicrowave.Domain.Entities;
using DigitalMicrowave.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMicrowave.Application.Services
{
    public class HeatingProgramService
    {
        private readonly IHeatingProgramRepository _repo;
        public HeatingProgramService(IHeatingProgramRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<HeatingProgram> GetAll()
        {
            return _repo.GetAll();
        } 

        public void Create(HeatingProgram program)
        {
            Validate(program);
            _repo.Create(program);
        }

        public void Update(HeatingProgram program)
        {
            Validate(program, program.Id);
            _repo.Update(program);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }

        private void Validate(HeatingProgram program, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(program.ProgramName))
                throw new Exception("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(program.Food))
                throw new Exception("Alimento é obrigatório");

            if (program.Time <= 0)
                throw new Exception("Tempo deve ser maior que zero");

            if (program.Power <= 0)
                throw new Exception("Potência inválida");

            if (string.IsNullOrWhiteSpace(program.HeatingCharacteristic))
                throw new Exception("Caractere de aquecimento obrigatório");

            if (program.HeatingCharacteristic == ".")
                throw new Exception("O caractere de aquecimento não pode ser '.'");

            if (_repo.CharacterExists(program.HeatingCharacteristic, ignoreId))
                throw new Exception("Este caractere de aquecimento já está em uso!");
        }
    }
}
