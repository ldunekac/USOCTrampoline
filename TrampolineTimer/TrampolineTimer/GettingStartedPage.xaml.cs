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
using System.Diagnostics;

namespace TrampolineTimer
{
    /// <summary>
    /// Interaction logic for GettingStartedPage.xaml
    /// </summary>
    public partial class GettingStartedPage : Page
    {
        internal List<Coach> coaches = new List<Coach>();
        internal List<Athlete> athletes = new List<Athlete>();
        public string gymName;
        public string gymLocation;

        public GettingStartedPage()
        {
            InitializeComponent();
            StartCoachGrid();
            StartAthleteGrid();

            Debug.WriteLine(gymLocationTextBox.GetType().Namespace);
            gymNameTextBox.TextChanged += (sender, e) => gymName = gymNameTextBox.Text;
            gymLocationTextBox.TextChanged += (sender, e) => gymLocation = gymLocationTextBox.Text;
        }

        private void StartCoachGrid() {
            coaches.Add(new Coach());
            addCoachEntriesFor(coaches[0]);
        }

        private void StartAthleteGrid() {
            athletes.Add(new Athlete());
            addAthleteEntriesFor(athletes[0]);
        }

        // The following two methods implement automatically growing data entry grids.
        private void addCoachEntriesFor(Coach coach) {
            var firstNameEntry = new TextBox { Style = (Style) Resources["GridEntry"] };
            firstNameEntry.TextChanged += (sender, e) => {
                coach.FirstName = firstNameEntry.Text;
                ensureEmptyCoachEntry();
            };
            Grid.SetColumn(firstNameEntry, 0);

            var lastNameEntry = new TextBox { Style = (Style)Resources["GridEntry"] };
            lastNameEntry.TextChanged += (sender, e) => {
                coach.LastName = lastNameEntry.Text;
                ensureEmptyCoachEntry();
            };
            Grid.SetColumn(lastNameEntry, 1);

            coachesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            Grid.SetRow(firstNameEntry, coachesGrid.RowDefinitions.Count - 1);
            Grid.SetRow(lastNameEntry, coachesGrid.RowDefinitions.Count - 1);

            coachesGrid.Children.Add(firstNameEntry);
            coachesGrid.Children.Add(lastNameEntry);
        }

        private void addAthleteEntriesFor(Athlete athlete) {
            var firstNameEntry = new TextBox { Style = (Style)Resources["GridEntry"] };
            firstNameEntry.TextChanged += (sender, e) => {
                athlete.FirstName = firstNameEntry.Text;
                ensureEmptyAthleteEntry();
            };
            Grid.SetColumn(firstNameEntry, 0);

            var lastNameEntry = new TextBox { Style = (Style)Resources["GridEntry"] };
            lastNameEntry.TextChanged += (sender, e) => {
                athlete.LastName = lastNameEntry.Text;
                ensureEmptyAthleteEntry();
            };
            Grid.SetColumn(lastNameEntry, 1);

            var ageEntry = new TextBox { Style = (Style)Resources["GridEntry"] };
            ageEntry.TextChanged += (sender, e) => {
                athlete.Age = ageEntry.Text;
                ensureEmptyAthleteEntry();
            };
            Grid.SetColumn(ageEntry, 2);

            var weightEntry = new TextBox { Style = (Style)Resources["GridEntry"] };
            weightEntry.TextChanged += (sender, e) => {
                athlete.Weight = weightEntry.Text;
                ensureEmptyAthleteEntry();
            };
            Grid.SetColumn(weightEntry, 3);

            var heightEntry = new TextBox { Style = (Style)Resources["GridEntry"] };
            heightEntry.TextChanged += (sender, e) => {
                athlete.Height = heightEntry.Text;
                ensureEmptyAthleteEntry();
            };
            Grid.SetColumn(heightEntry, 4);

            athletesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            Grid.SetRow(firstNameEntry, athletesGrid.RowDefinitions.Count - 1);
            Grid.SetRow(lastNameEntry, athletesGrid.RowDefinitions.Count - 1);
            Grid.SetRow(ageEntry, athletesGrid.RowDefinitions.Count - 1);
            Grid.SetRow(weightEntry, athletesGrid.RowDefinitions.Count - 1);
            Grid.SetRow(heightEntry, athletesGrid.RowDefinitions.Count - 1);

            athletesGrid.Children.Add(firstNameEntry);
            athletesGrid.Children.Add(lastNameEntry);
            athletesGrid.Children.Add(ageEntry);
            athletesGrid.Children.Add(weightEntry);
            athletesGrid.Children.Add(heightEntry);
        }

        private void ensureEmptyAthleteEntry() {
            if (athletes.Any(c => c.IsEmpty)) return;

            var athlete = new Athlete();
            athletes.Add(athlete);
            addAthleteEntriesFor(athlete);
        }

        private void ensureEmptyCoachEntry() {
            if (coaches.Any(c => c.IsEmpty)) return;

            var coach = new Coach();
            coaches.Add(coach);
            addCoachEntriesFor(coach);
        }

        private void doneButton_Click(object sender, RoutedEventArgs e) {
            // Done, save to the database and move to the athletes page
            DBAdapter.Instance.InsertIntoGymTable(gymName, gymLocation);
            DBAdapter.Instance.InsertIntoAthletesTable(new List<Athlete>(athletes.Where(c => !c.IsEmpty)));
            DBAdapter.Instance.InsertIntoCoachesTable(new List<Coach>(coaches.Where(c => !c.IsEmpty)));

            var p = new AthletesPage();
            this.NavigationService.Navigate(p);
        }
    }
}
