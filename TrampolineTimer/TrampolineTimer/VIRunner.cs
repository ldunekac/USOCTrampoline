using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InteropAssembly;
using System.Diagnostics;

namespace TrampolineTimer {
    class VIRunner {
        public void Run() {
            // Calls the data reading VI, will block until explicitly stopped
            LabVIEWExports.DataReader();
        }

        public void Stop() {
            // Sets the 'Running' global variable in the VI to false
            LabVIEWExports.Stop();
        }
    }
}
