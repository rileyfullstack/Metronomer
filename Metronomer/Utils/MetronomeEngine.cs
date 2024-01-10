using Metronomer.Utils.Practices;
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
        public SoundManager _soundManager = new SoundManager();
        private int _bpm = 100; // default value
        private int _division = 1; // default to quarter notes
        private bool _isStressed; //Current streesed note check
        private int _divisionIndex = 1; //Used by the change division index method.
        private bool _allowStart = false; //Adds a flag to control the start
        public delegate void NoteIndicatorHandler(int index = 0);
        public event NoteIndicatorHandler NoteIndicator; //Event to trigger the visual note indication in the MainWIndow.
        public string? _practiceType = null; //Practice type will be here. If its null, then there is no practice going on.
        private PracticeChangingNotesClass? practiceChangeingNotes;

        public MetronomeEngine()
        {
            _timer = new Timer { AutoReset = true };
            _timer.Elapsed += OnTimedEvent;
            UpdateInterval();
        }

        public void Start()
        {
            _divisionIndex = 1; //Makes sure the first note will always be stressed, when unpaused.
            ChangeDivisionStress(); //Updates the stress indicator acording to the index ^
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
        public void NullifyPracticeClasses() 
        //Nullifies all of the practice classes before a change of practice type.
        //Add more as you add more practices.
        {
            practiceChangeingNotes = null;
        }
        public void ChangePractice(string? practiceType) //If the practice type is null then it reverts to being a normal metronome.
        {
            NullifyPracticeClasses();
            _practiceType = practiceType;
            switch (_practiceType)
            {
                case "practiceChangeingNotes":
                    practiceChangeingNotes = new PracticeChangingNotesClass();
                    break;
            }
        }
        public void AllowStart()
        {
            _allowStart = true;
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
            if(_practiceType != null) return;
            Stop(); //Makes sure to stop and then start again so there are no bugs related to the division update.
            NoteIndicator?.Invoke();//Turns the note indicators gray in enticipation for the change, so they don't have to wait for the sound to play to turn gray.
            _division = division; //Updates the division value.
            UpdateInterval(); //Updates the interval to match the new division value.
            if (_allowStart) //This is to make sure the app doesn't activate this method on startup.
            {
                Start();
            }
        }
        private void PrivateSetNoteDivision(int division)
        {
            Stop(); //Makes sure to stop and then start again so there are no bugs related to the division update.
            NoteIndicator?.Invoke();//Turns the note indicators gray in enticipation for the change, so they don't have to wait for the sound to play to turn gray.
            _division = division; //Updates the division value.
            UpdateInterval(); //Updates the interval to match the new division value.
            if (_allowStart) //This is to make sure the app doesn't activate this method on startup.
            {
                Start();
            }
        }
        private void UpdateInterval()
        {
            _timer.Interval = (60.0 / _bpm) * 1000 / _division;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            switch (_practiceType)
            {
                case null:
                    {
                        PlayNormalNote();
                    }
                    break;
                case "practiceChangeingNotes":
                    {
                        PlayPracticeChangeNotesNote();
                    }
                    break;
            }
        }

        private void PlayNormalNote()
        {
            PlayNote();
            if (!(_division == 1)) NoteIndicator?.Invoke(_divisionIndex); // Raise the note indicator event with the current division index if the _division is not equal to 1.
            if (_division != 1)
            {
                ChangeDivisionIndex();
                ChangeDivisionStress();
            }
        }
        private void PlayPracticeChangeNotesNote()
        {
            PlayNote();
            if (_division == _divisionIndex) //If the division has reached its final note
            {
                PrivateSetNoteDivision(practiceChangeingNotes._nextSubdivision); //Change the note division according to the next subdivision in the practice
                practiceChangeingNotes.SetNextSubdivision(); //Sets a random next subdivision
            }
            ChangeDivisionIndex();
            ChangeDivisionStress();
        }
        private void PlayNote()
        {
            _soundManager.PlayNote(_isStressed ? "ns" : "s");
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
