using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace TrampolineTimer.Data
{
    public class Session
    {
        public int Id { get; set; }
        public Athlete Athlete { get; set; }
        public Coach Coach { get; set; }
        public DateTime StartTime { get; set; }
        public string VideoFilename { get; set; }

        public double totalScore { get; set; }

        public double score { get; set; }
        // Might not really need to be an ObservableCollection
        public ObservableCollection<Jump> Jumps = new ObservableCollection<Jump>();

        public Session(Athlete athlete)
        {
            Athlete = athlete;
        }

    }
}
