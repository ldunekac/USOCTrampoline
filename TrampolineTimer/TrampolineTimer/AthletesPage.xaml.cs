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

namespace TrampolineTimer {
    /// <summary>
    /// Interaction logic for AthletesPage.xaml
    /// </summary>
    public partial class AthletesPage : Page {
        public AthletesPage() {
            DataContext = DBAdapter.Instance;
            InitializeComponent();
        }

        private void Athlete_Click(object sender, MouseButtonEventArgs e) {
            // Casts athlete clicked to FrameworkElement
            var ele = e.OriginalSource as FrameworkElement;
            // Fails quietly upon its untimely demise
            if (ele == null) return;

            // Gets athlete based on element clicked
            var athlete = ele.DataContext as Athlete;
            // Fails less quietly, with much gurgling and feet-kicking
            if (athlete == null) return;

            var p = new SkillsPage(athlete);
            this.NavigationService.Navigate(p);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var p = new GettingStartedPage();
            this.NavigationService.Navigate(p);
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            var p = new SearchPage();
            this.NavigationService.Navigate(p);
        }
    }
}
