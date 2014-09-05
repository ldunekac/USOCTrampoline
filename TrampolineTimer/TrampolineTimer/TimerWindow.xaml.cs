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
using System.Windows.Shapes;
using System.Windows.Navigation;
using TrampolineTimer.Data;

namespace TrampolineTimer {
    /// <summary>
    /// Interaction logic for TimerWindow.xaml
    /// </summary>
    public partial class TimerWindow : NavigationWindow {
        public TimerWindow() {
            InitializeComponent();
            Page p;
            if (DBAdapter.Instance.GymName == null)
            {
                p = new GettingStartedPage();
            }
            else
            {
                p = new AthletesPage();
            }
            this.NavigationService.Navigate(p);
        }
    }
}
