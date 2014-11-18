using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InteropAssembly;

namespace TrampolineTimer {
    class VIClient {
        bool running;

        public VIClient() {
            running = false;
        }

        public void Stop() {
            running = false;
        }

        public void Run() {
            // Awkward way to deal with running becoming false while in ReadTime, feel free to refactor
            running = true;
            Returnvalue? data = null;
            while (running) {
                App.Current.Dispatcher.BeginInvoke(new Action(() => {
                    // Checking for a timeout inside the VI
                    if (data.HasValue && data.Value.timestampMs != 9001)
                    {
                        ((App)App.Current).EventReceived(data.Value.onTrampoline, data.Value.timestamp.AddMilliseconds(data.Value.timestampMs));
                    }
                }));

                // Reads in data from the VI as a cluster, will block
                data = LabVIEWExports.ReadEvent();
            }
        }
    }
}
