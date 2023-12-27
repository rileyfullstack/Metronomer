using System;
using System.Collections.Generic;
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
        private string? _semiStressedNoteSource;
        private string? _nonStressedNoteSource;
        private SoundPlayer? _currentPlayer; // To hold the currently playing sound

        public SoundManager()
        {
            _stressedNoteSource = "Audio/MetronomeNotes/MetronomeStandard_High.wav";
            _semiStressedNoteSource = null;
            _nonStressedNoteSource = "Audio/MetronomeNotes/MetronomeStandard_Low.wav";
        }

        public void LoadMetronomeSounds(string title)
        {
            string jsonText = File.ReadAllText("TextResources/metronome-paths.json");
            var metronomePaths = JsonSerializer.Deserialize<MetronomePathsCollection>(jsonText);

            var metronome = metronomePaths?.MetronomePaths.FirstOrDefault(m => m.Title == title);
            if (metronome != null)
            {
                _stressedNoteSource = metronome.Paths["Stressed"];
                _semiStressedNoteSource = metronome.Paths["Semi-Stressed"];
                _nonStressedNoteSource = metronome.Paths["Not-Stressed"];
            }
            else
            {
                // Handle the case where the title is not found
                Console.WriteLine("Metronome title not found.");
            }
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
                case "ss":
                    path = _semiStressedNoteSource;
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

    public class MetronomePathsCollection
    {
        public List<MetronomePaths> MetronomePaths { get; set; }
    }
    public class MetronomePaths
    {
        public string Title { get; set; }
        public Dictionary<string, string?> Paths { get; set; }
    }
}
