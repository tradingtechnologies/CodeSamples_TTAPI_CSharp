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
            // Confirm TTAPI installation archetecture.
            AboutDTS.TTAPIArchitectureCheck();

            using (Dispatcher disp = Dispatcher.AttachUIDispatcher())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Create an instance of TTAPI.
                frmMarketExplorer marketExplorer = new frmMarketExplorer();
                ApiInitializeHandler handler = new ApiInitializeHandler(marketExplorer.ttApiInitHandler);
                TTAPI.CreateXTraderModeTTAPI(disp, handler);

                Application.Run(marketExplorer);
            }
        }

    }
}
