using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrampolineTimer.Data
{
    public class Jump
    {
        public DateTime StartTime { get; set; }
        public double Length { get; set; }
        public DateTime EndTime
        {
            get
            {
                return StartTime.AddSeconds(Length);
            }
            set
            {
                Length = (value - StartTime).TotalSeconds;
            }
        }
        public Position StartPosition { get; set; }
        public Position EndPosition { get; set; }

        public class Position
        {
            public double x;
            public double y;
        }
    }
}
