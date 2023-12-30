using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Metronomer.Utils
{
    public class SoundManager
    {
        private string? _stressedNoteSource;
        private string? _nonStressedNoteSource;
        private SoundPlayer? _currentPlayer; // To hold the currently playing sound

        public SoundManager()
        {
            _stressedNoteSource = "Audio/MetronomeNotes/MetronomeStandard_Low.wav";
            _nonStressedNoteSource = "Audio/MetronomeNotes/MetronomeStandard_High.wav";
        }

        public void ChangeMetronomeSound(string[] paths)
        {

            _stressedNoteSource = paths[0];
            _nonStressedNoteSource = paths[1];
        }

        // Implement note playing here.
        public void PlayNote(string stressLevel)
        {
            string? path = null;
            switch (stressLevel)
            {
                case "s":
                    path = _stressedNoteSource;
                    break;
                case "ns":
                    path = _nonStressedNoteSource;
                    break;
                default:
                    Console.WriteLine("Invalid stress level.");
                    return;
            }

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    // Stop the current sound if one is playing
                    _currentPlayer?.Stop();

                    // Dispose the previous player and create a new one
                    _currentPlayer?.Dispose();
                    _currentPlayer = new SoundPlayer(path);
                    _currentPlayer.Play(); // Play the new sound
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error playing sound: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("No path specified for the given stress level.");
            }
        }
    }
}
