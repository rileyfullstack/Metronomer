using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metronomer.Utils.Practices
{
    internal class PracticeChangingNotesClass
    {
        private Random random = new Random();
        public int _nextSubdivision { get; set; }

        public PracticeChangingNotesClass(int nextSubdivision = 1)
        {
            _nextSubdivision = nextSubdivision;
        }

        public void SetNextSubdivision()
        {
            do
            {
                _nextSubdivision = random.Next(1, 5); //Chooses next subdivision between 1 and 4.
            }
            while (_nextSubdivision == 3); //If the number is 3, try again.
        }
    }
}
