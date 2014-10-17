using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrampolineTimer.Data;

namespace TrampolineTimer
{
    /// <summary>
    /// Interaction logic for Skills.xaml
    /// </summary>
    /// 
    public partial class SkillsPage : Page
    {
        private Athlete athlete;
        private SkillList skillList;
        public SkillsPage(Athlete athlete)
        {
            this.athlete = athlete;
            skillList = new SkillList();
            InitializeComponent();

            for (var i = 0; i < 10; i++)
            {
                addSkillEntriesFor();
            }
        }

        private void addSkillEntriesFor(/*Skill skill */)
        {
            var figShortHand = new TextBox { Style = (Style)Resources["GridEntry"] };
            Grid.SetColumn(figShortHand, 0);

            var skillType = new ComboBox { Style = (Style)Resources["GridEntry"] };
            var skillstring = skillList.skillListToString(skillList.getSkillList()).ToArray();
            foreach (var s in skillstring)
            {
                skillType.Items.Add(s);
            }

            Grid.SetColumn(skillType, 1);

            var rotation = new TextBox { Style = (Style)Resources["GridEntry"] };
            Grid.SetColumn(rotation, 2);
            
            var sommersault = new TextBox { Style = (Style)Resources["GridEntry"] };
            Grid.SetColumn(sommersault, 3);
            
            var twist = new TextBox { Style = (Style)Resources["GridEntry"] };
            Grid.SetColumn(twist, 4);

            var baseDD = new TextBox { Style = (Style)Resources["GridEntry"] };
            baseDD.IsEnabled = false;
            Grid.SetColumn(baseDD, 5);

            var total = new TextBox { Style = (Style)Resources["GridEntry"] };
            total.IsEnabled = false;
            Grid.SetColumn(total, 6);

            figShortHand.TextChanged += (sender, e) => {
                skillType.Items.Clear();
 
                var subSkillList = skillList.getSkillList(figShortHand.Text);
                var skillListAsString = skillList.skillListToString(subSkillList).ToArray(); ;
                foreach (var s in skillListAsString)
                {
                    skillType.Items.Add(s);
                }
                if (skillType.Items.Count > 0)
                {
                    skillType.SelectedItem = skillType.Items[0];
                    skillType.Text = skillType.Items[0].ToString();
                }
                else
                {
                    baseDD.Text = "";
                }
                total.Text = (toFloat(baseDD.Text) + toFloat(rotation.Text) * 0.1f + toFloat(sommersault.Text) * 0.1f + toFloat(twist.Text) * 0.1f).ToString();
            };

            skillType.SelectionChanged += (sender, e) =>
            {
                Console.Write(e.Source.ToString());
                if ((sender as ComboBox).Items.Count <= 0) return;
                var selectedSkill = this.skillList.getSkillByName((sender as ComboBox).SelectedItem.ToString());
                if (selectedSkill != null)
                {
                    baseDD.Text = selectedSkill.dd.ToString();
                }
                total.Text = (toFloat(baseDD.Text) + toFloat(rotation.Text) * 0.1f + toFloat(sommersault.Text) * 0.1f + toFloat(twist.Text) * 0.1f).ToString();
            };

            rotation.TextChanged += (sender, e) =>
            {
                total.Text = (toFloat(baseDD.Text) + toFloat(rotation.Text) * 0.1f + toFloat(sommersault.Text) * 0.1f + toFloat(twist.Text) * 0.1f).ToString();
            };

            sommersault.TextChanged += (sender, e) =>
            {
                total.Text = (toFloat(baseDD.Text) + toFloat(rotation.Text) * 0.1f + toFloat(sommersault.Text) * 0.1f + toFloat(twist.Text) * 0.1f).ToString();
            };

            twist.TextChanged += (sender, e) =>
            {
                total.Text = (toFloat(baseDD.Text) + toFloat(rotation.Text) * 0.1f + toFloat(sommersault.Text) * 0.1f + toFloat(twist.Text) * 0.1f).ToString();
            };


            skillsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            Grid.SetRow(figShortHand, skillsGrid.RowDefinitions.Count - 1);
            Grid.SetRow(skillType, skillsGrid.RowDefinitions.Count - 1);
            Grid.SetRow(rotation, skillsGrid.RowDefinitions.Count - 1);
            Grid.SetRow(sommersault, skillsGrid.RowDefinitions.Count - 1);
            Grid.SetRow(twist, skillsGrid.RowDefinitions.Count - 1);
            Grid.SetRow(baseDD, skillsGrid.RowDefinitions.Count - 1);
            Grid.SetRow(total, skillsGrid.RowDefinitions.Count - 1);

            skillsGrid.Children.Add(figShortHand);
            skillsGrid.Children.Add(skillType);
            skillsGrid.Children.Add(rotation);
            skillsGrid.Children.Add(sommersault);
            skillsGrid.Children.Add(twist);
            skillsGrid.Children.Add(baseDD);
            skillsGrid.Children.Add(total);

        }


        private float toFloat(string s)
        {
            try
            {
                return float.Parse(s);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            var p = new TimingPage(athlete);
            this.NavigationService.Navigate(p);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var p = new AthletesPage();
            this.NavigationService.Navigate(p);
        }
    }
}
