using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TradingTechnologies.TTAPI;

// Utility is a static class defining the ExtensionMethods BeginInvokeIfRequired and InvokeIfRequired.
// Please refer to MSDN for more information about ExtensionMethods. http://msdn.microsoft.com/en-us/library/bb383977
// 
// Lambda expressions are used with the ExtensionMethods within the test application.
// Please refer to MSDN for more information about Lambda expressions. http://msdn.microsoft.com/en-us/library/bb397687 and http://msdn.microsoft.com/en-us/library/bb383984
//
// Lambda expressions are used to query collections within the test application.
// Please refer to MSDN for more information about Lambda expressions used to Query collections. http://msdn.microsoft.com/en-us/library/bb397675

namespace TTAPI_Samples
{
    static class Utility
    {
        /// <summary>
        /// Dispatcher ExtensionMethod to BeginInvoke only if required.
        /// </summary>
        public static void BeginInvokeIfRequired(this Dispatcher dispatcher, Action action)
        {
            if (dispatcher != null && !dispatcher.IsDisposed)
            {
                if (dispatcher.CheckAccess())
                {
                    action();
                }
                else
                {
                    dispatcher.BeginInvoke(action);
                }
            }
        }

        /// <summary>
        /// Dispatcher ExtensionMethod to Invoke only if required.
        /// </summary>
        public static void InvokeIfRequired(this Dispatcher dispatcher, Action action)
        {
            if (dispatcher != null && !dispatcher.IsDisposed)
            {
                if (dispatcher.CheckAccess())
                {
                    action();
                }
                else
                {
                    dispatcher.Invoke(action);
                }
            }
        }
    }
}
