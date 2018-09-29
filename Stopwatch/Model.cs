using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyStopwatch
{
    /// <summary>
    /// A lap entry used as a model in this stopwatch implementation
    /// </summary>
    public class Lap
    {
        // Maybe its good to re-do class with fields and add like time & lap number;
        public string LapEntry { get; set; }

        // this constructor might be useful with refactoring
        public Lap(TimeSpan elapsed, int lapNum)
        {
            var lapTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                elapsed.Hours, elapsed.Minutes, elapsed.Seconds, elapsed.Milliseconds);
            LapEntry = $"{lapNum}. {lapTime}";
        }

        // this constructor is used for convenience in add command
        public Lap(string elapsed, int lapNum)
        {
            LapEntry = $"{lapNum}. {elapsed}";
        }
    }
}
