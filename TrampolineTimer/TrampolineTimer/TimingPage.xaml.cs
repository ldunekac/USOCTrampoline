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

using System.Collections.ObjectModel;
using TrampolineTimer.Data;

namespace TrampolineTimer {
    /// <summary>
    /// Interaction logic for TimingPage.xaml
    /// </summary>
    public partial class TimingPage : Page {
        public Session Session { get; private set; }
        public List<Jump> jumpList;
        public List<Coach> Coaches
        {
            get
            {
                return DBAdapter.Instance.Coaches;
            }
        }
        bool recording = false;
        bool existingSession;
        CameraInterface form;

        public TimingPage() {
            InitializeComponent();
            DataContext = this;

            ((App)App.Current).ResetJump();
            ((App)App.Current).NewJump += TimingPage_NewJump;
        }

        public TimingPage(Data.Athlete athlete, List<Jump> jumpList) : this()
        {
            this.Session = new Session(athlete);
            existingSession = false;
            this.jumpList = jumpList;
        }

        public TimingPage(Data.Session session)
            : this()
        {
            // Not currently used, but allows to load in an existing session
            this.Session = session;
            existingSession = true;
        }

        void TimingPage_NewJump(object sender, NewJumpEventArgs e)
        {
            if (!recording) return;

            bars.Children.Clear();
            bars.ColumnDefinitions.Clear();

            Session.Jumps.Add(e.Jump);
            double maxSize = Session.Jumps.Max(time => time.Length);

            foreach (var jump in Session.Jumps)
            {
                var bar = new Border
                {
                    Style = Resources["barStyle"] as Style,
                    Height = (jump.Length / maxSize) * bars.ActualHeight,
                };

                var text = new TextBlock
                {
                    Text = jump.Length.ToString("F2"),
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                };

                bar.Child = text;

                Grid.SetRow(bar, 0);
                ColumnDefinition column = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                Grid.SetColumn(bar, bars.ColumnDefinitions.Count);
                bars.ColumnDefinitions.Add(column);
                bars.Children.Add(bar);

                barsScroll.ScrollToRightEnd();
            }
        }

        private void video_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            form = new CameraInterface(this.Session);
            form.TopLevel = false;

            host.Child = form;

            // Add the interop host control to the Grid 
            // control's collection of child controls. 
            this.grid1.Children.Add(host);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current).NewJump -= TimingPage_NewJump;
        }

        private void controlButton_Click(object sender, RoutedEventArgs e)
        {
            if (controlButton.Content.Equals("Replay"))
            {
                form.StartVideoPlayback();
            }
            else
            {
                if (recording)
                {
                    form.StopCapturing();
                    recording = false;
                    DBAdapter.Instance.SaveSession(Session);
                    existingSession = true;
                    controlButton.Content = "Replay";
                    backButton.IsEnabled = true;
                }
                else
                {
                    if (Session.Coach == null)
                    {
                        MessageBox.Show("Please select a coach!");
                        return;
                    }
                    form.StartCapturing();
                    recording = true;
                    controlButton.Content = "Stop";
                    Session.StartTime = DateTime.Now;
                    backButton.IsEnabled = false;
                }
            }

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            form.StopCapturing();
            form.StopVideoPlayback();
            form.Dispose();

            var p = new AthletesPage();
            this.NavigationService.Navigate(p);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Gets coach based on element clicked
            Session.Coach = e.AddedItems[0] as Coach;
        }
    }
}
