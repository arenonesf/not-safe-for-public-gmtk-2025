using UnityEngine;

namespace Code.Core.Timer
{
    public class CountdownTimer : Timer
    {
        public CountdownTimer(float value) : base(value) { }

        public override bool IsFinished => CurrentTime <= 0;

        public override void Tick()
        {
            if(IsRunning && CurrentTime > 0)
            {
                CurrentTime -= Time.deltaTime;
            }

            if(IsRunning && CurrentTime <= 0 ) 
            { 
                Stop(); 
            }
        }
    }
}

