using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TTAPI_Sample_FillSubscription
{
    class Program
    {
        /// <summary>
        /// This TT API sample application demonstrates how to subscribe for fills.
        /// It is intended to be used only as an illustrative example and should not be run in a production environment.
        /// </summary>
        /// 

        static void Main(string[] args)
        {
            // Create a separate thread on which to run TT API
            TTAPIFunctions tf = new TTAPIFunctions();
            Thread workerThread = new Thread(tf.Start);
            workerThread.Name = "TT API Thread";
            workerThread.Start();

            // Insert other code here that will run on this thread
        }
    }
}
