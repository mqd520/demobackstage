using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Common
{
    /// <summary>
    /// CountDown, precision: second
    /// </summary>
    public class CountDown
    {
        private int time = 0;
        private int remainTime = 0;
        private Timer t = new Timer();

        /// <summary>
        /// Count Down Complete Event
        /// </summary>
        /// CountDown:  CountDown
        /// int:        Time
        /// int:        RemainTime
        public event Action<CountDown, int, int> Complete;


        /// <summary>
        /// CountDown
        /// </summary>
        /// <param name="time">time(precision: second)</param>
        public CountDown(int time)
        {
            this.time = time;
            this.remainTime = time;
            t.Interval = 1000;
            t.Elapsed += OnElapsed;
        }

        protected void OnElapsed(object sender, ElapsedEventArgs e)
        {
            remainTime--;
           
            if (remainTime < 0)
            {
                remainTime = 0;
            }

            //Console.WriteLine(remainTime);

            OnRemainTimeChanged();
        }

        protected void OnRemainTimeChanged()
        {
            if (remainTime == 0)
            {
                t.Stop();

                if (Complete != null)
                {
                    Complete.BeginInvoke(this, time, remainTime, null, null);
                }
            }
        }

        /// <summary>
        /// Get or Set Time
        /// </summary>
        public int Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                remainTime = value;
            }
        }

        public int RemainTime
        {
            get
            {
                return remainTime;
            }
        }

        /// <summary>
        /// Get Completed
        /// </summary>
        public bool IsCompleted
        {
            get
            {
                return remainTime == 0;
            }
        }

        public void Start()
        {
            remainTime = time;
            if (time > 0)
            {
                t.Start();
            }
        }

        public void Stop()
        {
            t.Stop();
            remainTime = time;
        }

        public void Restart()
        {
            t.Stop();
            t.Start();
        }
    }
}
