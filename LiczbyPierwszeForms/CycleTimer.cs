using System;

namespace LiczbyPierwszeForms
{
    public class CycleTimer
    {
        public DateTime StartCycleTime { get; set; }

        public void StartNewCycle()
        {
            StartCycleTime = DateTime.Now;
        }

        public TimeSpan GetCycleTimer()
        {
            return DateTime.Now - StartCycleTime;
        }

        public TimeSpan GetBreakTimer()
        {
            return DateTime.Now - StartCycleTime;
        }
    }
}
