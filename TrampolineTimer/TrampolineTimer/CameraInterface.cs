using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.DirectX.AudioVideoPlayback;
using Microsoft.DirectX;

using DirectX.Capture;
using DShowNET;

using TrampolineTimer.Data;

namespace TrampolineTimer
{
    /** 
     * Interface to display camera in the timing page 
     * Uses a form interface that is dropped in the xaml in TimingPage.xaml.cs
     */
    public partial class CameraInterface : Form
    {
        // Used to capture video in preview and to file
        Capture capture = null;
        // Used to capture system filters and compress video
        Filters filters = null;

        // Defines a new Video object for replay
        Video replayVideo = null;

        // Sets the device number to choose
        // TODO: This should be updated to allow the user to choose any device connected
        int deviceNumber = 0;
        // Path to the local folder that will contain the current athlete
        string localFilePath;
        // Local path with the current video name appended
        string videoFilePath;
        // Captures the current session for system information
        // Allows us to grab things like athletes and coaches
        Session session;

        /** 
         * Constructor for camera interface
         * Takes the current session as input
         */
        public CameraInterface(Session session)
        {
            // Initialize form
            InitializeComponent();
            // Sets the current session to a local variable
            this.session = session;

            // Get the current directory, and append the athletes first and last name
            localFilePath = System.Environment.CurrentDirectory + "\\" + session.Athlete.FirstName + session.Athlete.LastName;
            // Create a directory at that path if it doesn't already exist
            // This handles both the creation and moving on if it already exists
            Directory.CreateDirectory(localFilePath);
        }

        /**
         * Built-in form creation method to show the form
         */
        private void CameraInterface_Shown(object sender, EventArgs e)
        {
            // Initialize filters to use the system filters
            filters = new Filters();

            // Preview if it captured a video device
            if (filters.VideoInputDevices != null)
            {
                try
                {
                    // Initialize preview in the form for the pre-defined device number
                    Preview(deviceNumber);
                }
                catch (Exception ex)
                {
                    // Else, warn the user that their web cam is probably in use
                    MessageBox.Show("Your web cam appears to be in use.\n\n Error Message: \n\n" + ex.Message);
                }
            }
            else
            {
                // If no input device, warn user to connect device before continuing
                MessageBox.Show("No video device connected to your PC!");
            }
        }

        /**
         * Preview from device in form window
         */
        void Preview(int deviceNo)
        {
            try
            {
                // Set our capture to our currently connected device
                // NOTE: null is where audio would be captured. We determined this probably wasn't needed,
                // but implementing it there would allow audio capture as well
                capture = new Capture(filters.VideoInputDevices[deviceNo], null);

                // Compress video using predetermined filter
                // We did some research on this; this filter seemed to compress best, but that doesn't mean it is the best
                capture.VideoCompressor = filters.VideoCompressors[1];
                // Set the video panel as our initial preview (before capture actually begins)
                capture.PreviewWindow = cameraPanel;
            }
            catch { }
        }

        /**
         * Implement to replace preview and begin capturing video
         */
        public void StartCapturing()
        {
            try
            {
                if (!capture.Cued)
                {
                    // Set our filename of where to save if we haven't cued up the video yet
                    videoFilePath = localFilePath + "\\" + DateTime.Now.ToString("MM-dd-yy H-mm-ss") + ".wmv";
                    // Set our defined video path to the captures filename location and save
                    capture.Filename = videoFilePath;
                }
                // Cue the video to start capturing
                capture.Cue();
                // Start video capture
                capture.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: \n\n" + ex.Message);
            }
        }

        /**
         * Implement to stop capturing video
         */
        public void StopCapturing()
        {
            // If capture is currently going, stop it
            if (capture != null) capture.Stop();
            // Clear the panels controls
            cameraPanel.Controls.Clear();
        }

        /**
         * Initialize video review playback
         */
        public void StartVideoPlayback()
        {
            // Get the width and height of the panel for playback
            int width = cameraPanel.Width;
            int height = cameraPanel.Height;

            try
            {
                // Set the new video replay to our current video path
                replayVideo = new Video(videoFilePath);
                // Set the virtual owner of our replay to the panel in our interface form
                replayVideo.Owner = cameraPanel;
                // Start playing
                replayVideo.Play();
                // Set the size to our saved width and height
                cameraPanel.Size = new Size(width, height);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        /**
         * Stop video review playback
         */
        private void StopVideoPlayback(object sender, System.EventArgs e)
        {
            if (replayVideo != null)
            {
                // If video isn't null, stop playing back
                replayVideo.Stop();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}