using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using TradingTechnologies.TTAPI;

namespace TTAPI_Samples
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // confirm TTAPI installation archetecture
            AboutDTS.TTAPIArchitectureCheck();

            XTraderModeTTAPIOptions envOptions = new XTraderModeTTAPIOptions();
            // Enable or Disable the TT API Implied Engine
            envOptions.EnableImplieds = false;

            // Create and attach a UI Dispatcher to the main Form
            // When the form exits, this scoping block will auto-dispose of the Dispatcher
            using (var disp = Dispatcher.AttachUIDispatcher())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Create an instance of TTAPI.
                frmPriceUpdateMultiThreaded priceUpdateMultiThreaded = new frmPriceUpdateMultiThreaded();
                ApiInitializeHandler handler = new ApiInitializeHandler(priceUpdateMultiThreaded.ttApiInitHandler);
                TTAPI.CreateXTraderModeTTAPI(disp, handler);

                Application.Run(priceUpdateMultiThreaded);
            }
        }
    }
}
