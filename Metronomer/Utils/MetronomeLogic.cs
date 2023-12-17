using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Metronomer.Utils
{
    internal class MetronomeLogic
    {
        private double Bpm { get; set; }
        private DispatcherTimer metronomeTimer;
        private string subDivision;

        public MetronomeLogic()
        {
            Bpm = 100; //inital bpm is always 100
            subDivision = "Qater"; //initial subdivision is alwayer a Qater.
        }

        private void SetTimerInterval(double bpm)
        {
            double interval = 60000 / bpm; // Converts BPM to milliseconds per beat
            metronomeTimer.Interval = TimeSpan.FromMilliseconds(interval);
            metronomeTimer.Tick += OnTimedEvent;
            SetTimerInterval(Bpm);
        }

        public void Start()
        {
            metronomeTimer.Start();
        }

        public void Stop()
        {
            metronomeTimer.Stop();
        }

        public void SetBpm(double bpm)
        {
            Bpm = bpm;
            SetTimerInterval(Bpm);
        }

        private void OnTimedEvent(Object sender, EventArgs e)
        {
            // Your code to handle the metronome tick
            Console.WriteLine("Tick");
        }
    }
}
