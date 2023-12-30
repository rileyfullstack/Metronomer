using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Metronomer.Utils
{
    internal class SoundsJsonDeserializer
    {
        private MetronomePathsCollection _metronomePathsCollection;

        public SoundsJsonDeserializer()
        {
            FillSoundPaths();
        }

        private void FillSoundPaths() //Fills the metronome paths on startup.
        {
            string jsonText = File.ReadAllText("TextResources/metronome-paths.json");
            if (!string.IsNullOrEmpty(jsonText))
            {
                _metronomePathsCollection = JsonSerializer.Deserialize<MetronomePathsCollection>(jsonText);
            }
            else Console.WriteLine("The JSON file containing the sound paths is empty.");
        }

        public string[] ReturnSoundTitles() //Returns the titles of the sound paths to be selected by the user.
        {
            if (_metronomePathsCollection == null)
            {
                throw new Exception("The collection of paths for the sounds is null.");
            }
            return _metronomePathsCollection.MetronomePaths
            .Select(metronomePath => metronomePath.Title)
            .ToArray();
        }
        public string[] ReturnPathsByTitle(string title)
        {
            if (_metronomePathsCollection == null)
            {
                throw new Exception("The collection of paths for the sounds is null.");
            }
            var matchingMetronomePath = _metronomePathsCollection.MetronomePaths
                                  .FirstOrDefault(mp => mp.Title == title);
            return matchingMetronomePath.Paths.Values.Where(p => p != null).ToArray();
        }
    }
    internal class MetronomePathsCollection
    {
        public List<MetronomePaths> MetronomePaths { get; set; }
    }
    internal class MetronomePaths
    {
        public string Title { get; set; }
        public Dictionary<string, string?> Paths { get; set; }
    }
}
