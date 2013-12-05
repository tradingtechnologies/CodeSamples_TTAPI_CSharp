using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TTAPI_Sample_Console_OrderRouting
{
    class Program
    {
        static void Main(string[] args)
        {
            string ttUserId = "JSMITH";
            string ttPassword = "12345678";

            // Check that the compiler settings are compatible with the version of TT API installed
            TTAPIArchitectureCheck archCheck = new TTAPIArchitectureCheck();
            if (archCheck.validate())
            {
                Console.WriteLine("Architecture check passed.");

                // Dictates whether TT API will be started on its own thread
                bool startOnSeparateThread = false;

                if (startOnSeparateThread)
                {
                    // Start TT API on a separate thread
                    TTAPIFunctions tf = new TTAPIFunctions(ttUserId, ttPassword);
                    Thread workerThread = new Thread(tf.Start);
                    workerThread.Name = "TT API Thread";
                    workerThread.Start();

                    // Insert other code here that will run on this thread
                }
                else
                {
                    // Start the TT API on the same thread
                    using (TTAPIFunctions tf = new TTAPIFunctions(ttUserId, ttPassword))
                    {
                        tf.Start();
                    }
                }
            }
            else
            {
                Console.WriteLine("Architecture check failed.  {0}", archCheck.ErrorString);
            }
        }
    }
}
