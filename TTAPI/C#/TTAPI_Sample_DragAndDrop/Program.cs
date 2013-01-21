using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TTAPI_Sample_DragAndDrop
{
    using TradingTechnologies.TTAPI;

    static class Program
    {
        /// <summary>
        /// This TT API sample application demonstrates how to drag-and-drop contracts from X_TRADER to this TT API application.
        /// It is intended to be used only as an illustrative example and should not be run in a production environment.
        /// </summary>

        [STAThread]
        static void Main()
        {
            // Attach a UIDispatcher to the current thread
            using (UIDispatcher disp = Dispatcher.AttachUIDispatcher())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Form1 f = new Form1();

                // Use "Follow X_TRADER" Login Mode
                TTAPI.XTraderModeDelegate xtDelegate = new TTAPI.XTraderModeDelegate(f.ttApiInitComplete);
                TTAPI.CreateXTraderModeTTAPI(Dispatcher.Current, xtDelegate);

                Application.Run(f);

                // Shutdown the TT API
                f.shutdownTTAPI();
            }
        }
    }
}
