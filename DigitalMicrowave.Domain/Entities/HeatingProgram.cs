using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMicrowave.Domain.Entities
{
    public class HeatingProgram
    {
        public int Id { get; set; }
        public string ProgramName { get; set; }
        public string Food { get; set; }
        public int Power { get; set; }
        public string HeatingCharacteristic { get; set; } // caractere único
        public string Instructions { get; set; }
        public int Time { get; set; } // segundos
        public bool ProgramDefault { get; set; } // true = pré-definido
        public bool IsCustom => !ProgramDefault;
    }
}
