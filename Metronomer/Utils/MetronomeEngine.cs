using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace Metronomer.Utils
{
    public class MetronomeEngine
    {
        private Timer _timer;
        private SoundManager _soundManager = new SoundManager();
        private int _bpm = 100; // default value
        private int _division = 1; // default to quarter notes
        private bool _isStressed; //Current streesed note check
        private int _divisionIndex = 1; //Used by the change division method. Explanation in the method.

        public MetronomeEngine()
        {
            _timer = new Timer { AutoReset = true };
            _timer.Elapsed += OnTimedEvent;
            UpdateInterval();
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void SetBpm(int bpm)
        {
            if (bpm > 0)
            {
                _bpm = bpm;
                UpdateInterval();
            }
        }

        public void SetNoteDivision(int division)
        {
            _division = division;
            UpdateInterval();
        }

        private void UpdateInterval()
        {
            _timer.Interval = (60.0 / _bpm) * 1000 / _division;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            _soundManager.PlayNote(_isStressed ? "s" : "ns");
            if(_division != 1)
            {
                ChangeDivisionIndex();
                ChangeDivisionStress();
            }
        }

        //Changes the _divisionIndex based on whether the index has reached the division or not.
        //First, it changes the index. If it had reached the division, it will restart back to 1. Otherwise, it will increase by one.
        private void ChangeDivisionIndex()
        {
            if (_divisionIndex != _division)
            {
                _divisionIndex++;
            }
            else _divisionIndex = 1;
        }

        //Changes the stress of the note based on whether the index is 0 or not.
        //If it is, that means its the first note in the subdivision, and needs to be stressed.
        private void ChangeDivisionStress()
        {
            if (_divisionIndex == 1)
            {
                _isStressed = true;
            }
            else _isStressed = false;
        }
    }
}
