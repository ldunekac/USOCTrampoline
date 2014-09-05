using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrampolineTimer.Data {
    public class Athlete
    {
        // These have to be properties so XAML can access them
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }

        public Athlete() {
            FirstName = "";
            LastName = "";
            Age = "";
            Height = "";
            Weight = "";
        }

        public bool IsEmpty {
            get {
                return FirstName.Equals("") && LastName.Equals("") && Age.Equals("") && Height.Equals("") && Weight.Equals("");
            }
        }
    }
}
