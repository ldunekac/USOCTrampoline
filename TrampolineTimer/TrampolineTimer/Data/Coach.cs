using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrampolineTimer.Data {
    public class Coach
    {
        // These have to be properties so XAML can access them
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Coach()
        {
            FirstName = "";
            LastName = "";
        }

        public bool IsEmpty {
            get { return FirstName.Equals("") && LastName.Equals(""); }
        }
    }
}
