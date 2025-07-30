using System.Collections.Generic;

namespace Code.Core.Timer
{
    public static class TimerManager
    {
        static readonly List<Timer> timers = new();

        public static void AddTimer(Timer timer) => timers.Add(timer);
        public static void RemoveTimer(Timer timer) => timers.Remove(timer);

        public static void UpdateTimers()
        {
            foreach (Timer timer in new List<Timer>(timers))
            {
                timer.Tick();
            }
        }

        public static void Clear() => timers.Clear();
    }
}
