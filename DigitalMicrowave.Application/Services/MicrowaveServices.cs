using DigitalMicrowave.Domain.Entities;
using DigitalMicrowave.Domain.Enums;
using DigitalMicrowave.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMicrowave.Application.Services
{
    public class MicrowaveServices
    {
        private Microwave _microwave = new Microwave();
        public Microwave Heated => _microwave;        
        public void Start(int seconds, int power)
        {
            ValidateHeating(seconds, power);
            if (_microwave.State == MicrowaveStateEnum.Running)
            {
                Add30Seconds();
                return;
            }

            _microwave.Power = power == 0 ? 10 : power;
            _microwave.TotalSeconds = seconds;
            _microwave.RemainingSeconds = _microwave.TotalSeconds;

            _microwave.State = MicrowaveStateEnum.Running;


        }      
        
        private void ValidateHeating(int seconds, int power)
        {
            if (seconds == 0)
                throw new System.Exception("Informe o tempo  do aquecimento.");
            if (seconds < 1 || seconds > 120)
                throw new System.Exception("Tempo inválido! Informe um tempo entre 1 e 120 segundos.");          
            if (power < 0 || power > 10)
                throw new System.Exception("Potência inválida! Informe uma potência entre 1 e 10.");
        }

        public void PauseOrCancel()
        {
            if (_microwave.State == MicrowaveStateEnum.Running)
            {
                _microwave.State = MicrowaveStateEnum.Paused;
            }
            else 
            {
                Cancel();
            }
        }

        public void ProcessTick(string heatingChar)
        {
            if (_microwave.State != MicrowaveStateEnum.Running) return;

            _microwave.RemainingSeconds--;

            _microwave.ProgressText = !string.IsNullOrEmpty(heatingChar) ? heatingChar : HeatingFormatter(_microwave.TotalSeconds - _microwave.RemainingSeconds, _microwave.Power, _microwave.TotalSeconds);

            if (_microwave.RemainingSeconds <= 0)
            {
                _microwave.State = MicrowaveStateEnum.Finished;
                _microwave.ProgressText +=" Aquecimento concluído";
            }
        }

        public string GetFormattedTime()
        {
            return Utils.GetFormattedTime(_microwave.TotalSeconds);
        }

        public void Resume()
        {
            if (_microwave.State == MicrowaveStateEnum.Paused)
                _microwave.State = MicrowaveStateEnum.Running;
        }

        private void Cancel()
        {
            _microwave.State = MicrowaveStateEnum.Cancelled;
            _microwave.TotalSeconds = 0;
            _microwave.RemainingSeconds = 0;
            _microwave.ProgressText = "";
        }

        private string HeatingFormatter(int elapsed, int power, int total)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < elapsed; i++)
                sb.Append(new string('.', power) + " ");
            return sb.ToString().Trim();
        }

        private void Add30Seconds()
        {
            _microwave.RemainingSeconds += 30;
            _microwave.TotalSeconds += 30;
            if (_microwave.RemainingSeconds > 120) 
                _microwave.RemainingSeconds = 120;
        }
    }
}
