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
using System.ComponentModel;
using System.Collections.Generic;

namespace TrampolineTimer
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        private List<User> items;
        ListSortDirection direction = ListSortDirection.Ascending;
        public SearchPage()
        {
            InitializeComponent();
            items = new List<User>();
            showAllSessions();
        }

        private void showAllSessions()
        {
            List<Session> sessions = DBAdapter.Instance.SelectSessionAthleteCoach();

            foreach(var session in sessions)
            {
                items.Add(new User()
                {
                    Athlete = session.Athlete.FirstName + " " + session.Athlete.LastName,
                    Coach = session.Coach.FirstName + " " + session.Coach.LastName,
                    Date = DateTime.Now.ToString(),
                    Extra = session.totalScore.ToString()
                });
            }
            searchGrid.ItemsSource = items;
        }

        public class User
        {
            public string Athlete { get; set; }

            public string Coach { get; set; }

            public string Date { get; set; }

            public string Extra { get; set; }
        }

        private void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;

            if (headerClicked != null)
            {
                if (direction == ListSortDirection.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else 
                {
                    direction = ListSortDirection.Ascending;
                }
                string header = headerClicked.Column.Header as string;
                Sort(header, direction);

            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(searchGrid.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Page p = new AthletesPage();
            this.NavigationService.Navigate(p);
        }
    }
}
