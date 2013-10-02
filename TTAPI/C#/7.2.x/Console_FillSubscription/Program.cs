// **********************************************************************************************************************
//
//	Copyright © 2005-2013 Trading Technologies International, Inc.
//	All Rights Reserved Worldwide
//
// 	* * * S T R I C T L Y   P R O P R I E T A R Y * * *
//
//  WARNING: This file and all related programs (including any computer programs, example programs, and all source code) 
//  are the exclusive property of Trading Technologies International, Inc. (“TT”), are protected by copyright law and 
//  international treaties, and are for use only by those with the express written permission from TT.  Unauthorized 
//  possession, reproduction, distribution, use or disclosure of this file and any related program (or document) derived 
//  from it is prohibited by State and Federal law, and by local law outside of the U.S. and may result in severe civil 
//  and criminal penalties.
//
// ************************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TTAPI_Sample_Console_FillSubscription
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
