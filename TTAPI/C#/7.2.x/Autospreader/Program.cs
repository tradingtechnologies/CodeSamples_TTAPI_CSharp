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

            using (Dispatcher dispatcher = Dispatcher.AttachUIDispatcher())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Create an instance of TTAPI.
                AutospreaderManagerForm autospreaderManagerForm = new AutospreaderManagerForm();
                TTAPI.XTraderModeDelegate xtDelegate = new TTAPI.XTraderModeDelegate(autospreaderManagerForm.initTTAPI);
                TTAPI.CreateXTraderModeTTAPI(dispatcher, xtDelegate);

                Application.Run(autospreaderManagerForm);
            }
        }
    }
}
