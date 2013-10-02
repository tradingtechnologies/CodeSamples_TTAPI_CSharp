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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.WinFormsHelpers;

namespace TTAPI_Samples
{
    /// <summary>
    /// PriceUpdateMultiThreaded
    /// 
    /// This example demonstrates using the TT API to retrieve market data from 
    /// multiple instruments.  Each PriceSubscription will be on it's own thread.
    /// 
    /// NOTE:   This is NOT a best practice for creating a multi-threaded application
    ///         and TT does NOT recommend using the threading model observed in this 
    ///         code.  The code should be used for illustrative purposes to provide an 
    ///         example of how to use multiple threads with the TT API.  
    ///         
    ///         Marshaling from a worker thread to the GUI for the purposes of output 
    ///         is expensive, and for best results should be limited.
    /// </summary>
    public partial class frmPriceUpdateMultiThreaded : Form
    {
        private const int MAX_INSTRUMENT_COUNT = 4;

        // Declare the TTAPI objects.
        private XTraderModeTTAPI m_TTAPI = null;
        private Dictionary<InstrumentKey, InstrumentModel> m_bindingModels = null;
        private bool m_isShutdown = false, m_shutdownInProcess = false;

        public frmPriceUpdateMultiThreaded()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Init and start TT API.
        /// </summary>
        /// <param name="instance">XTraderModeTTAPI instance</param>
        /// <param name="ex">Any exception generated from the ApiCreationException</param>
        public void ttApiInitHandler(TTAPI api, ApiCreationException ex)
        {
            if (ex == null)
            {
                m_TTAPI = (XTraderModeTTAPI)api;
                m_TTAPI.ConnectionStatusUpdate += new EventHandler<ConnectionStatusUpdateEventArgs>(ttapiInstance_ConnectionStatusUpdate);
                m_TTAPI.Start();
            }
            else if (!ex.IsRecoverable)
            {
                MessageBox.Show("API Initialization Failed: " + ex.Message);
            }
        }

        /// <summary>
        /// ConnectionStatusUpdate callback.
        /// Give feedback to the user that there was an issue starting up and connecting to XT.
        /// </summary>
        void ttapiInstance_ConnectionStatusUpdate(object sender, ConnectionStatusUpdateEventArgs e)
        {
            if (e.Status.IsSuccess)
            {
                this.Enabled = true;
            }
            else
            {
                MessageBox.Show(String.Format("ConnectionStatusUpdate: {0}", e.Status.StatusMessage));
            }
        }

        /// <summary>
        /// Dispose of all the TT API objects and shutdown the TT API 
        /// </summary>
        public void shutdownTTAPI()
        {
            if (!m_shutdownInProcess)
            {
                // Dispose of all request objects
                foreach (InstrumentModel model in m_bindingModels.Values)
                {
                    // Ask each thread dispatcher to shutdown.
                    model.Dispatcher.BeginInvokeShutdown();
                }

                TTAPI.ShutdownCompleted += new EventHandler(TTAPI_ShutdownCompleted);
                TTAPI.Shutdown();
            }

            m_shutdownInProcess = true;
        }

        /// <summary>
        /// Event fired when the TT API has been successfully shutdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TTAPI_ShutdownCompleted(object sender, EventArgs e)
        {
            m_isShutdown = true;
            Close();
        }

        /// <summary>
        /// Suspends the FormClosing event until the TT API has been shutdown
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!m_isShutdown)
            {
                e.Cancel = true;
                shutdownTTAPI();
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

        #region Drag and Drop

        /// <summary>
        /// Form drag over event handler.
        /// The form must enable "AllowDrop" for these events to fire.
        /// </summary>
        private void frmPriceUpdateMultiThreaded_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.HasInstrumentKeys())
                e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// Form drag and drop event handler.
        /// The form must enable "AllowDrop" for these events to fire.
        /// </summary>
        private void frmPriceUpdateMultiThreaded_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.HasInstrumentKeys())
                FindInstrument(e.Data.GetInstrumentKeys());
        }

        #endregion

        /// <summary>
        /// Function to find a list of InstrumentKeys.
        /// </summary>
        /// <param name="keys">List of InstrumentKeys.</param>
        private void FindInstrument(IList<InstrumentKey> keys)
        {
            if ((keys.Count > 0) && (keys.Count <= MAX_INSTRUMENT_COUNT))
            {
                SetComponentText(toolStripStatusLabel1, "Drag & Drop detected.  Initializing instrument...");

                // dispose of any current instrument models
                if (m_bindingModels != null)
                {
                    foreach (var entry in m_bindingModels)
                    {
                        entry.Value.ThreadID = "";
                        entry.Value.Exchange = "";
                        entry.Value.Product = "";
                        entry.Value.ProdType = "";
                        entry.Value.Contract = "";
                        entry.Value.BidPrice = "";
                        entry.Value.AskPrice = "";
                        entry.Value.LastPrice = "";

                        entry.Value.Dispatcher.BeginInvokeShutdown();
                    }
                }

                // Release any current data bindings
                ReleaseDataBinding();

                // (re) instantiate our binding instrument model
                m_bindingModels = new Dictionary<InstrumentKey, InstrumentModel>(MAX_INSTRUMENT_COUNT);

                // index assigned to each instrument which corresponds to the GUI output
                int index = 1;
                foreach (InstrumentKey key in keys)
                {
                    Console.WriteLine(String.Format("TT API FindInstrument {0}", key.ToString()));

                    // create the model for the instrument & set the component data bindings
                    InstrumentModel model = new InstrumentModel(this);
                    SetDataBinding(index++, model);

                    // add the instrument model to our lookup table
                    m_bindingModels.Add(key, model);

                    // Instrument lookups are all still on the main application thread
                    InstrumentLookupSubscription instrRequest = new InstrumentLookupSubscription(m_TTAPI.Session, Dispatcher.Current, key);
                    instrRequest.Update += instrRequest_Completed;
                    instrRequest.Start();
                }
            }
            else
            {
                MessageBox.Show("This application accepts a maximum of " + MAX_INSTRUMENT_COUNT + " contracts.");
            }
        }

        /// <summary>
        /// Instrument request completed event.
        /// </summary>
        void instrRequest_Completed(object sender, InstrumentLookupSubscriptionEventArgs e)
        {
            if (e.IsFinal && e.Instrument != null)
            {
                try
                {
                    // Instrument lookup is valid, create the price subscription on it's own thread
                    Console.WriteLine(String.Format("TT API FindInstrument {0}", e.Instrument.Name));
                    SetComponentText(toolStripStatusLabel1, "Instrument Found.");

                    // set static data in our instrument model (bindings will automatically update the GUI)
                    m_bindingModels[e.Instrument.Key].Exchange = e.Instrument.Product.Market.Name;
                    m_bindingModels[e.Instrument.Key].Product = e.Instrument.Product.Name;
                    m_bindingModels[e.Instrument.Key].ProdType = e.Instrument.Product.Type.ToString();
                    m_bindingModels[e.Instrument.Key].Contract = e.Instrument.GetFormattedName(InstrumentNameFormat.User);

                    // create a new thread for the price subscription
                    Thread createDispatcherThread = new Thread(CreateDispatcher);
                    createDispatcherThread.Start(e.Instrument);
                }
                catch (Exception err)
                {
                    Console.WriteLine(String.Format("TT API FindInstrument Exception: {0}", err.Message));
                }
            }
            else if (e.IsFinal)
            {
                Console.WriteLine(String.Format("TT API FindInstrument Instrument Not Found: {0}", e.Error));
            }
            else
            {
                Console.WriteLine(String.Format("TT API FindInstrument Instrument Not Found: (Still Searching) {0}", e.Error));
            }
        }

        /// <summary>
        /// Creates and runs a new dispatcher for each PriceSubscription thread
        /// </summary>
        /// <param name="instrument">TT API Instrument</param>
        private void CreateDispatcher(object instrument)
        {
            using (WorkerDispatcher dispatcher = Dispatcher.AttachWorkerDispatcher())
            {
                // set the dispatcher to the associated model
                Instrument instr = instrument as Instrument;
                m_bindingModels[instr.Key].Dispatcher = dispatcher;
                m_bindingModels[instr.Key].ThreadID = Thread.CurrentThread.ManagedThreadId.ToString();
                m_bindingModels[instr.Key].StartPriceSubscription(instr);

                // start routing messages for this thread
                dispatcher.Run();

                // Dispose of the InstrumentModel if the dispatcher is shutdown
                m_bindingModels[instr.Key].Dispose();
            }
        }

        /// <summary>
        /// Binds the controls text property to the corresponding InstrumentModel property
        /// </summary>
        /// <param name="index">index of which set of controls to bind</param>
        /// <param name="model">model to bind the controls to</param>
        private void SetDataBinding(int index, InstrumentModel model)
        {
            if (index == 1)
            {
                this.txtThreadID_1.DataBindings.Add("Text", model, "ThreadID", true);
                this.txtExchange_1.DataBindings.Add("Text", model, "Exchange", true);
                this.txtProduct_1.DataBindings.Add("Text", model, "Product", true);
                this.txtProdType_1.DataBindings.Add("Text", model, "ProdType", true);
                this.txtContract_1.DataBindings.Add("Text", model, "Contract", true);
                this.txtBidPrice_1.DataBindings.Add("Text", model, "BidPrice", true);
                this.txtAskPrice_1.DataBindings.Add("Text", model, "AskPrice", true);
                this.txtLastPrice_1.DataBindings.Add("Text", model, "LastPrice", true);
            }
            else if (index == 2)
            {
                this.txtThreadID_2.DataBindings.Add("Text", model, "ThreadID", true);
                this.txtExchange_2.DataBindings.Add("Text", model, "Exchange", true);
                this.txtProduct_2.DataBindings.Add("Text", model, "Product", true);
                this.txtProdType_2.DataBindings.Add("Text", model, "ProdType", true);
                this.txtContract_2.DataBindings.Add("Text", model, "Contract", true);
                this.txtBidPrice_2.DataBindings.Add("Text", model, "BidPrice", true);
                this.txtAskPrice_2.DataBindings.Add("Text", model, "AskPrice", true);
                this.txtLastPrice_2.DataBindings.Add("Text", model, "LastPrice", true);
            }
            else if (index == 3)
            {
                this.txtThreadID_3.DataBindings.Add("Text", model, "ThreadID", true);
                this.txtExchange_3.DataBindings.Add("Text", model, "Exchange", true);
                this.txtProduct_3.DataBindings.Add("Text", model, "Product", true);
                this.txtProdType_3.DataBindings.Add("Text", model, "ProdType", true);
                this.txtContract_3.DataBindings.Add("Text", model, "Contract", true);
                this.txtBidPrice_3.DataBindings.Add("Text", model, "BidPrice", true);
                this.txtAskPrice_3.DataBindings.Add("Text", model, "AskPrice", true);
                this.txtLastPrice_3.DataBindings.Add("Text", model, "LastPrice", true);
            }
            else if (index == 4)
            {
                this.txtThreadID_4.DataBindings.Add("Text", model, "ThreadID", true);
                this.txtExchange_4.DataBindings.Add("Text", model, "Exchange", true);
                this.txtProduct_4.DataBindings.Add("Text", model, "Product", true);
                this.txtProdType_4.DataBindings.Add("Text", model, "ProdType", true);
                this.txtContract_4.DataBindings.Add("Text", model, "Contract", true);
                this.txtBidPrice_4.DataBindings.Add("Text", model, "BidPrice", true);
                this.txtAskPrice_4.DataBindings.Add("Text", model, "AskPrice", true);
                this.txtLastPrice_4.DataBindings.Add("Text", model, "LastPrice", true);
            }
        }

        /// <summary>
        /// Releases all existing data bindings
        /// </summary>
        private void ReleaseDataBinding()
        {
            this.txtThreadID_1.DataBindings.Clear();
            this.txtExchange_1.DataBindings.Clear();
            this.txtProduct_1.DataBindings.Clear();
            this.txtProdType_1.DataBindings.Clear();
            this.txtContract_1.DataBindings.Clear();
            this.txtBidPrice_1.DataBindings.Clear();
            this.txtAskPrice_1.DataBindings.Clear();
            this.txtLastPrice_1.DataBindings.Clear();

            this.txtThreadID_2.DataBindings.Clear();
            this.txtExchange_2.DataBindings.Clear();
            this.txtProduct_2.DataBindings.Clear();
            this.txtProdType_2.DataBindings.Clear();
            this.txtContract_2.DataBindings.Clear();
            this.txtBidPrice_2.DataBindings.Clear();
            this.txtAskPrice_2.DataBindings.Clear();
            this.txtLastPrice_2.DataBindings.Clear();

            this.txtThreadID_3.DataBindings.Clear();
            this.txtExchange_3.DataBindings.Clear();
            this.txtProduct_3.DataBindings.Clear();
            this.txtProdType_3.DataBindings.Clear();
            this.txtContract_3.DataBindings.Clear();
            this.txtBidPrice_3.DataBindings.Clear();
            this.txtAskPrice_3.DataBindings.Clear();
            this.txtLastPrice_3.DataBindings.Clear();

            this.txtThreadID_4.DataBindings.Clear();
            this.txtExchange_4.DataBindings.Clear();
            this.txtProduct_4.DataBindings.Clear();
            this.txtProdType_4.DataBindings.Clear();
            this.txtContract_4.DataBindings.Clear();
            this.txtBidPrice_4.DataBindings.Clear();
            this.txtAskPrice_4.DataBindings.Clear();
            this.txtLastPrice_4.DataBindings.Clear();
        }

        /// <summary>
        /// Event which opens the About window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            AboutDTS aboutForm = new AboutDTS();
            aboutForm.ShowDialog(this);
        }

        #region InstrumentModel

        /// <summary>
        /// Class whose properties are bound to the respective WinForms control's Text
        /// property.  When the setter is called for a property in this class, the 
        /// corresponding text field will be updated.
        /// 
        /// This will also store the dispatcher and price subscription for the corresponding
        /// instrument.
        /// </summary>
        public class InstrumentModel : INotifyPropertyChanged, IDisposable
        {
            // store the parent form for thread marshaling
            private readonly Form m_ParentForm = null;
            delegate void PropertyChangedCallback(string info);

            private WorkerDispatcher m_dispatcher = null;
            private PriceSubscription m_priceSubscription = null;

            // event to fire so the bound control knows when to update
            public event PropertyChangedEventHandler PropertyChanged;

            private bool m_disposed = false;
            private object m_lock = new object();

            private string m_ThreadID;
            private string m_Exchange;
            private string m_Product;
            private string m_ProdType;
            private string m_Contract;

            private string m_BidPrice;
            private string m_AskPrice;
            private string m_LastPrice;

            /// <summary>
            /// Class constructor
            /// </summary>
            /// <param name="parent">Parent form which owns the controls to update</param>
            public InstrumentModel(Form parent)
            {
                m_ParentForm = parent;
            }

            ~InstrumentModel()
            {
                Dispose(false);
            }

            public void Dispose()
            {
                Dispose(true);

                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!m_disposed)
                {
                    if (disposing)
                    {
                        if (m_priceSubscription != null)
                        {
                            m_priceSubscription.FieldsUpdated -= priceSubscription_FieldsUpdated;
                            m_priceSubscription.Dispose();
                            m_priceSubscription = null;
                        }
                    }

                    m_disposed = true;
                }
            }

            /// <summary>
            /// Set/Get Dispatcher.
            /// Add ShutdownStarted event when dispatcher is attached.
            /// </summary>
            public WorkerDispatcher Dispatcher 
            {
                get { return m_dispatcher; }
                set { m_dispatcher = value; }
            }

            /// <summary>
            /// Member of INotifyPropertyChanged, this will update the bound control's value
            /// </summary>
            /// <param name="info"></param>
            private void NotifyPropertyChanged(string info)
            {
                if (PropertyChanged != null)
                {
                    // Marshal back to the main application thread if needed
                    if (m_ParentForm.InvokeRequired)
                    {
                        PropertyChangedCallback propertyCB = new PropertyChangedCallback(NotifyPropertyChanged);
                        m_ParentForm.Invoke(propertyCB, new object[] { info });
                    }
                    else
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs(info));
                    }
                }
            }

            /// <summary>
            /// Create a price subscription
            /// </summary>
            /// <param name="instrument">Instrument to subscribe to</param>
            public void StartPriceSubscription(Instrument instrument)
            {
                m_priceSubscription = new PriceSubscription(instrument, m_dispatcher);
                m_priceSubscription.Settings = new PriceSubscriptionSettings(PriceSubscriptionType.InsideMarket);
                m_priceSubscription.FieldsUpdated += new FieldsUpdatedEventHandler(priceSubscription_FieldsUpdated);
                m_priceSubscription.Start();
            }

            /// <summary>
            /// Event where the price subscription fields have been updated.  Assign
            /// the updates values to the GUI.
            /// </summary>
            void priceSubscription_FieldsUpdated(object sender, FieldsUpdatedEventArgs e)
            {
                this.BidPrice = e.Fields.GetDirectBidPriceField().FormattedValue;
                this.AskPrice = e.Fields.GetDirectAskPriceField().FormattedValue;
                this.LastPrice = e.Fields.GetLastTradedPriceField().FormattedValue;
            }

            /*
             *  PUBLIC ACCESSOR METHODS
             */

            public string ThreadID
            {
                get { return m_ThreadID; }
                set
                {
                    m_ThreadID = value;
                    NotifyPropertyChanged("ThreadID");
                }
            }

            public string Exchange
            {
                get { return m_Exchange; }
                set
                {
                    m_Exchange = value;
                    NotifyPropertyChanged("Exchange");
                }
            }

            public string Product
            {
                get { return m_Product; }
                set
                {
                    m_Product = value;
                    NotifyPropertyChanged("Product");
                }
            }

            public string ProdType
            {
                get { return m_ProdType; }
                set
                {
                    m_ProdType = value;
                    NotifyPropertyChanged("ProdType");
                }
            }

            public string Contract
            {
                get { return m_Contract; }
                set
                {
                    m_Contract = value;
                    NotifyPropertyChanged("Contract");
                }
            }

            public string BidPrice
            {
                get { return m_BidPrice; }
                set
                {
                    m_BidPrice = value;
                    NotifyPropertyChanged("BidPrice");
                }
            }

            public string AskPrice
            {
                get { return m_AskPrice; }
                set
                {
                    m_AskPrice = value;
                    NotifyPropertyChanged("AskPrice");
                }
            }

            public string LastPrice
            {
                get { return m_LastPrice; }
                set
                {
                    m_LastPrice = value;
                    NotifyPropertyChanged("LastPrice");
                }
            }
        }

        #endregion

        #region SetComponentText
        /// <summary>
        /// Update the status bar and write the message to the console in a thread safe way.
        /// </summary>
        /// <param name="message">Message to update the status bar with.</param>
        delegate void SetComponentTextCallback(object control, string message);
        public void SetComponentText(object control, string message)
        {
            if (this.InvokeRequired)
            {
                SetComponentTextCallback componentCB = new SetComponentTextCallback(SetComponentText);
                this.Invoke(componentCB, new object[] { control, message });
            }
            else
            {
                // cast the control 
                dynamic ctrl = (dynamic)control;

                // Update the specified control.
                ctrl.Text = message;
            }
        }
        #endregion

    }
}