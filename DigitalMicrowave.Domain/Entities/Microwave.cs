using DigitalMicrowave.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMicrowave.Domain.Entities
{
    public class Microwave
    {
        public int Power { get; set; } = 10;
        public int TotalSeconds { get; set; }
        public bool IsRunning { get; set; }
        public bool IsPaused { get; set; }
        public MicrowaveStateEnum State { get; set; } = MicrowaveStateEnum.Ready;
        public string ProgressText { get; set; }
        public int RemainingSeconds { get;  set; }
    }
}
