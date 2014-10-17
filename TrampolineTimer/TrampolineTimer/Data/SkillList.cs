using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrampolineTimer.Data
{
    class SkillList
    {
        private static List<Skill> skillList;

        public SkillList()
        {
            if (skillList == null)
            {
                buildSkillList();
            }
        }
        void buildSkillList()
        {
            skillList = new List<Skill>();
            
            // front skills
            skillList.Add(new Skill("3/4 Front", "3", 0.3f));
            skillList.Add(new Skill("Arabian 3/4 Front", "31", 0.4f));
            
            skillList.Add(new Skill("Front Tuck", "4o", 0.5f));
            skillList.Add(new Skill("Front pike", "4>", 0.6f));
            skillList.Add(new Skill("Front Straight", "41/", 0.6f));

            skillList.Add(new Skill("Barani Tuck", "41o", 0.6f));
            skillList.Add(new Skill("Barani Pike", "41>", 0.6f));
            skillList.Add(new Skill("Barani Straight", "41/", 0.6f));

            skillList.Add(new Skill("Ballout", "5", 0.6f));
            skillList.Add(new Skill("Barani Ballout", "51", 0.7f));
            skillList.Add(new Skill("Rudy Ballout", "53", 0.9f));

            skillList.Add(new Skill("Front Full", "42", 0.7f));
            skillList.Add(new Skill("Rudi", "43", 0.8f));
            skillList.Add(new Skill("Front Double Full", "44", 0.9f));
            skillList.Add(new Skill("Randi", "45", 1.0f));

            skillList.Add(new Skill("1 3/4 Front Tuck", "7o", 0.8f));
            skillList.Add(new Skill("1 3/4 Front Pike", "7>", 0.9f));
            
            skillList.Add(new Skill("Double Front Tuck", "8000o", 1.0f));
            skillList.Add(new Skill("Double Front Pike", "800>", 1.2f));
            skillList.Add(new Skill("Double Front Straight", "800/", 1.2f));
            skillList.Add(new Skill("Half in back out Tuck", "810o", 1.1f));
            skillList.Add(new Skill("Half out Tuck", "801o", 1.1f));
            skillList.Add(new Skill("Half out Pike", "801>", 1.3f));
            skillList.Add(new Skill("Full Barani Tuck", "821o", 1.3f));
            skillList.Add(new Skill("Full Barani Straight", "821/", 1.5f));
            skillList.Add(new Skill("Rudi out Tuck", "803o", 1.3f));
            skillList.Add(new Skill("Rudi out Pike", "803>", 1.5f));

            skillList.Add(new Skill("Triffus Tuck", "12001o", 1.7f));
            skillList.Add(new Skill("Triffus Pike", "12001>", 2.0f));

            // Back skills
            
            skillList.Add(new Skill("3/4 Back", "3", 0.3f));

            skillList.Add(new Skill("Back Tuck", "4o", 0.5f));
            skillList.Add(new Skill("Back Pike", "4>", 0.6f));
            skillList.Add(new Skill("Back Straight", "4/", 0.6f));

            skillList.Add(new Skill("Tuck Cody", "5o", 0.6f));
            skillList.Add(new Skill("Pike Cody", "5>", 0.7f));
            skillList.Add(new Skill("Full Cody", "52", 0.8f));

            skillList.Add(new Skill("Back Full", "42", 0.7f));
            skillList.Add(new Skill("Back 1 1/2 Full", "43", 0.8f));
            skillList.Add(new Skill("Back Double Full", "44", 0.9f));
            skillList.Add(new Skill("Back Triple Full", "46", 1.1f));

            skillList.Add(new Skill("1 3/4 Back Tuck", "7o", 0.8f));
            skillList.Add(new Skill("1 3/4 Back Pike", "7>", 0.9f));

            skillList.Add(new Skill("Double Back Tuck", "800o", 1.0f));
            skillList.Add(new Skill("Double Back Pike", "800>", 1.2f));
            skillList.Add(new Skill("Double Back Straight", "800/", 1.2f));

            skillList.Add(new Skill("Full out Tuck", "802o", 1.2f));
            skillList.Add(new Skill("Full out Straight", "802/", 1.4f));
            skillList.Add(new Skill("Full Full Tuck", "822o", 1.4f));
            skillList.Add(new Skill("Full Full Straight", "822/", 1.6f));
            skillList.Add(new Skill("Half Half Tuck", "811o", 1.2f));
            skillList.Add(new Skill("Half Half Pike", "811>", 1.4f));
            skillList.Add(new Skill("Half Rudi Tuck", "813o", 1.4f));
            skillList.Add(new Skill("Half Rudi Pike", "813>", 1.6f));

            skillList = skillList.OrderBy(x => x.name).ToList();
        }

        public List<Skill> getSkillList(string shortHand = "")
        {
            return skillList.Where(x => x.figShortHand.StartsWith(shortHand)).ToList();
        }

        public List<string> skillListToString(List<Skill> skillList)
        { 
            List<string> rtn = new List<string>();
            foreach(var skill in skillList)
            {
                rtn.Add(skill.name);
            }
            return rtn;
        }

        public Skill getSkillByName(string skillName)
        {
            foreach (var s in skillList)
            {
                if (skillName == s.name)
                {
                    return s;
                }
            }
            return null;
        }
    }
}
