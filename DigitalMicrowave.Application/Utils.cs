using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMicrowave.Application
{
    public static class Utils
    {
        public static string GetFormattedTime(int seconds)
        {
            if (seconds >= 60)
            {
                var minutes = seconds / 60;
                var secondesAux = seconds % 60;
                return $"{minutes}:{secondesAux:D2}";
            }
            return seconds.ToString();
        }
    }
}
