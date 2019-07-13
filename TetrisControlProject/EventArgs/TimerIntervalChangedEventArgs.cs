using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisControlProject._EventArgs
{
    /// <summary>
    /// Gets info about new interval for timer.
    /// </summary>
    public class TimerIntervalChangedEventArgs
    {
        public int NewInterval {get;}
        public TimerIntervalChangedEventArgs(int newInterval)
        {
            this.NewInterval = newInterval;
        }
    }
}
