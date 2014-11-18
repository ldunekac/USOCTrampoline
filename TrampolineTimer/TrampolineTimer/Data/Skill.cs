using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrampolineTimer.Data
{
    public class Skill
    {
        public string figShortHand;
        public string name;
        public float dd;

        public Skill(string skillNname, string skillShorthand, float dd)
        {
            figShortHand = skillShorthand;
            name = skillNname;
            this.dd = dd;
        }
        
        public Skill()
        {
            figShortHand = "";
            name = "";
        }
    }
}
