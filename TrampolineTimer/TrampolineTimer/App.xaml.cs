using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.Threading;
using System.Collections.ObjectModel;
using TrampolineTimer.Data;

namespace TrampolineTimer {
    public class NewJumpEventArgs
    {
        public Jump Jump { get; set; }
    }

    public delegate void NewJumpEventHandler(object sender, NewJumpEventArgs e);

    public partial class App : Application {
        VIClient _Client = new VIClient();
        VIRunner _VI = new VIRunner();
        Thread _ClientThread;
        Thread _VIThread;
        Jump _curJump = null;

        public event NewJumpEventHandler NewJump;

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            _ClientThread = new Thread(new ThreadStart(_Client.Run));
            _ClientThread.Name = "VI Client";
            _ClientThread.Start();

            _VIThread = new Thread(new ThreadStart(_VI.Run));
            _VIThread.Name = "VI Runner";
            _VIThread.Start();
        }

        protected override void OnExit(ExitEventArgs e) {
            base.OnExit(e);

            _VI.Stop();
            _Client.Stop();
        }

        public void ResetJump()
        {
            // Called at the beginning of a jump sequence to ignore any trampoline landings that are
            // not being counted
            _curJump = null;
        }

        /**
         * Receives a landing or liftoff event and packages it into a new Jump.
         */
        internal void EventReceived(bool onTrampoline, DateTime timestamp)
        {
            if (onTrampoline)
            {
                // We landed, finish up the jump and notify any listeners
                if (_curJump == null) return;

                // Use a random position from [0, 1] x [0, 1] until the VI passes on position
                // information
                _curJump.EndPosition = new Jump.Position
                {
                    x = new Random().NextDouble(),
                    y = new Random().NextDouble(),
                };
                _curJump.EndTime = timestamp;
                if (NewJump != null) NewJump(this, new NewJumpEventArgs { Jump = _curJump } );
            }
            else
            {
                // We lifted off, create a new Jump
                _curJump = new Jump
                {
                    StartTime = timestamp,
                    StartPosition = new Jump.Position
                    {
                        x = new Random().NextDouble(),
                        y = new Random().NextDouble(),
                    }
                };
            }
        }
    }
}
