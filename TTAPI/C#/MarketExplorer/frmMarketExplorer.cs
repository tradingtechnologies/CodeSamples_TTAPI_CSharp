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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.WinFormsHelpers;
using System.Threading;
using System.IO;

namespace TTAPI_Samples
{
    /// <summary>
    /// MarketExplorer
    /// 
    /// This example demonstrates using the TT API to receive available exchanges, 
    /// products, and contract definitions.
    /// </summary>
    public partial class frmMarketExplorer : Form
    {
        // Declare the TTAPI objects.
        XTraderModeTTAPI m_apiInstance = null;
        Dispatcher m_dispatcher = null;
        ProductCatalogSubscription m_prodCat = null;
        Session m_session = null;
        MarketCatalog m_marketCatalog = null;

        // Private member variables
        Dictionary<Product, InstrumentCatalogSubscription> m_instrumentCatalogSubscriptionList = null;
        List<MarketListViewItem> m_marketList = null;
        List<FeedListViewItem> m_feedList = null;
        String m_searchFilter = null;

        public frmMarketExplorer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Init and start TT API.
        /// </summary>
        /// <param name="instance">XTraderModeTTAPI instance</param>
        /// <param name="ex">Any exception generated from the XTraderModeDelegate</param>
        public void initTTAPI(XTraderModeTTAPI apiInstance, Exception ex)
        {
            m_dispatcher = Dispatcher.Current;
            m_dispatcher.ShutdownStarted += m_dispatcher_ShutdownStarted;
            m_session = apiInstance.Session;

            m_apiInstance = apiInstance;
            m_apiInstance.ConnectionStatusUpdate += ttapiInstance_ConnectionStatusUpdate;
            m_apiInstance.ConnectToXTrader();
        }

        /// <summary>
        /// ConnectionStatusUpdate callback.
        /// Give feedback to the user that there was an issue starting up and connecting to XT.
        /// </summary>
        void ttapiInstance_ConnectionStatusUpdate(object sender, ConnectionStatusUpdateEventArgs e)
        {
            if (e.Status.IsSuccess)
            {
                m_marketList = new List<MarketListViewItem>();
                m_feedList = new List<FeedListViewItem>();

                // Multiple instruments can be opened in the propery window at the same time.
                // We need to keep track of all subscriptions to properly cleanup if needed.
                m_instrumentCatalogSubscriptionList = new Dictionary<Product, InstrumentCatalogSubscription>();

                // Init the windows.
                initWindowViews();

                // Attach to the MarketsUpdated event.
                m_marketCatalog = m_session.MarketCatalog;
                m_marketCatalog.MarketsUpdated += new EventHandler<MarketCatalogUpdatedEventArgs>(marketsUpdated);

                // Start the order and fill feeds so that they will be displayed in the "Market Feed Status" window.
                m_apiInstance.StartOrderFeed();
                m_apiInstance.StartFillFeed();
            }
            else
            {
                MessageBox.Show(String.Format("ConnectionStatusUpdate: {0}", e.Status.StatusMessage));
            }
        }


        #region MarketCatalog Markets

        /// <summary>
        /// MarketsUpdated callback.
        /// NOTE: MarketsUpdated will be called on the dispatcher that TT API was initialized on.
        /// </summary>
        private void marketsUpdated(object sender, MarketCatalogUpdatedEventArgs e)
        {
            listViewMarketList.Groups.Clear();
            listViewMarketList.Items.Clear();

            // Create a sorted list of markets objects.
            // Please refer to MSDN for more information about Lambda expressions used to Query collections. http://msdn.microsoft.com/en-us/library/bb397675
            var marketList = m_session.MarketCatalog.Markets.Values.OrderBy(market => market.Name);
            
            // Create new MarketListViewItems and add them to the view.
            foreach (Market market in marketList)
            {
                // Add all markets except for synthetic ones.
                if (market.Key != MarketKey.AlgoSE &&
                    market.Key != MarketKey.Autospreader)
                {
                    // Add the item if not currently in the tree
                    if (m_marketList.Where(m => m.Name == market.Name).Count() == 0)
                    {
                        MarketListViewItem marketItem = new MarketListViewItem(listViewMarketList);
                        marketItem.Market = market;
                        m_marketList.Add(marketItem);
                    }
                }
            }
        }

        /// <summary>
        /// MarketListViewItem is a subclass of ListViewItem.
        /// This allows us to have each market in the view self manage it's own events.
        /// These items will be used to update the current status of each market in the market ListView.
        /// </summary>
        public class MarketListViewItem : ListViewItem
        {
            private ListView m_parentView = null;
            private Market m_market = null;
            private Dispatcher m_dispatcher = null;

            /// <summary>
            /// Constructor for MarketListViewItem.
            /// </summary>
            /// <param name="parentView">Parent ListView.</param>
            public MarketListViewItem(ListView parentView)
            {
                // Set the parent view, current dispatcher, and attach to the Dispatcher.ShutdownStarted event.
                m_parentView = parentView;
                m_dispatcher = Dispatcher.Current;
                m_dispatcher.ShutdownStarted += new EventHandler<DispatcherShutdownEventArgs>(shutdownStarted);
            }

            /// <summary>
            /// Market property.
            /// This sets the market information in the ListViewItem such as Name, Tag, and Image.
            /// It also subscribes to the FeedStatusChanged events.
            /// </summary>
            public Market Market
            {
                get { return m_market; }
                set
                {
                    m_market = value;

                    Text = m_market.Name;   // this is what is displayed in the control
                    Name = m_market.Name;
                    Tag = m_market; 
                    ImageIndex = 0;         // Black

                    m_market.FeedStatusChanged += new EventHandler<FeedStatusChangedEventArgs>(feedStatusChanged);
                }
            }

            /// <summary>
            /// Update the feed status icon of this market seen in the markets ListView.
            /// </summary>
            private void feedStatusChanged(object sender, FeedStatusChangedEventArgs e)
            {
                // If this market is not in the markets ListView add it.
                if (m_parentView != null && !m_parentView.Items.Contains(this))
                {
                    m_parentView.Items.Add(this);
                    m_parentView.Sort();
                }

                var status = e.Feed.Status;
                switch (status.Availability)
                {
                    case FeedAvailability.Down:
                        ImageIndex = 0;
                        break;
                    case FeedAvailability.Up:
                        ImageIndex = 2;
                        break;
                }
            }

            /// <summary>
            /// Property to get the supported product types for a given market.
            /// </summary>
            public IEnumerable<ProductType> SupportedProductTypes
            {
                get
                {
                    if (m_market != null)
                        return m_market.SupportedProductTypes;
                    else
                        return null;
                }
            }

            #region Cleanup and Dispose
            public void Dispose()
            {
                // Dispose may be called on another thread due to garbage collection.
                // InvokeIfRequired is an extension method defined in Utility.cs within this project.
                // Please refer to Utility.cs for more information on what InvokeIfRequired is doing.
                m_dispatcher.InvokeIfRequired(() =>
                    {
                        if (m_market != null)
                        {
                            m_market.FeedStatusChanged -= feedStatusChanged;
                            m_market = null;
                        }
                    });

                m_parentView = null;
            }

            /// <summary>
            /// Dispatcher ShutdownStarted event.
            /// This is triggered when a dispatcher begins shutting down.
            /// </summary>
            private void shutdownStarted(object sender, DispatcherShutdownEventArgs e)
            {
                Dispose();
            }
            #endregion
        }

        #endregion

        #region Market Feeds
        
        /// <summary>
        /// FeedListViewItem is a subclass of ListViewItem.
        /// This allows us to have each feed in the view self manage it's own events.
        /// These items will be used to update the current status of each feed in the feed ListView.
        /// </summary>
        public class FeedListViewItem : ListViewItem
        {
            private ListView m_parentView = null;
            private Feed m_feed = null;
            private Dispatcher m_dispatcher = null;

            /// <summary>
            /// Constructor for FeedListViewItem.
            /// </summary>
            /// <param name="parentView">Parent ListView.</param>
            public FeedListViewItem(ListView parentView)
            {
                // Set the parent view, current dispatcher, and attach to the Dispatcher.ShutdownStarted event.
                m_parentView = parentView;
                m_dispatcher = Dispatcher.Current;
                m_dispatcher.ShutdownStarted += new EventHandler<DispatcherShutdownEventArgs>(shutdownStarted);
            }

            /// <summary>
            /// Feed property.
            /// This sets the feed information in the ListViewItem such as Name, Tag, and Image.
            /// It also subscribes to the FeedStatusChanged events.
            /// </summary>
            public Feed Feed
            {
                get { return m_feed; }
                set
                {
                    m_feed = value;

                    Text = m_feed.Name + " " + m_feed.GetType().Name;   // this is what is displayed in the control
                    Name = m_feed.Name + " " + m_feed.GetType().Name;
                    Tag = m_feed;
                    ImageIndex = 0;                                     // Black

                    // Attach to the FeedStatusChanged event and fire the even with the current state.
                    m_feed.FeedStatusChanged += new EventHandler<FeedStatusChangedEventArgs>(feedStatusChanged);
                    feedStatusChanged(m_feed, new FeedStatusChangedEventArgs(m_feed));
                }
            }

            /// <summary>
            /// Update the feed status icon of this Feed seen in the feeds ListView.
            /// </summary>
            private void feedStatusChanged(object sender, FeedStatusChangedEventArgs e)
            {
                // If this feed is not in the feeds ListView add it.
                if (m_parentView != null && !m_parentView.Items.Contains(this))
                {
                    m_parentView.Items.Add(this);
                    m_parentView.Sort();
                }

                switch (e.Feed.Status.Availability)
                {
                    case FeedAvailability.Down:
                        ImageIndex = 0;
                        break;
                    case FeedAvailability.Up:
                        ImageIndex = 2;
                        break;
                }
            }

            #region Cleanup and Dispose
            public void Dispose()
            {
            // Dispose may be called on another thread due to garbage collection.
            // InvokeIfRequired is an extension method defined in Utility.cs within this project.
            // Please refer to Utility.cs for more information on what InvokeIfRequired is doing.
                m_dispatcher.InvokeIfRequired(() =>
                {
                    if (m_feed != null)
                    {
                        m_feed.FeedStatusChanged -= feedStatusChanged;
                        m_feed = null;
                    }
                });

                m_parentView = null;
            }

            /// <summary>
            /// Dispatcher ShutdownStarted event.
            /// This is triggered when a dispatcher begins shutting down.
            /// </summary>
            private void shutdownStarted(object sender, DispatcherShutdownEventArgs e)
            {
                Dispose();
            }
            #endregion
        }

        #endregion

        #region ProductCatalogSubscription

        /// <summary>
        /// Create a ProductCatalogSubscription.
        /// </summary>
        /// <param name="market">Market to create subscription.</param>
        private void createProductSubscription(Market market)
        {
            m_prodCat = market.CreateProductCatalogSubscription(m_dispatcher);
            m_prodCat.ProductsUpdated += new EventHandler<ProductCatalogUpdatedEventArgs>(productsUpdated);
            m_prodCat.Start();
        }

        /// <summary>
        /// ProductCatalogSubscription ProductsUpdated event.
        /// This will update the product list in the product TreeView.
        /// </summary>
        /// <param name="sender">ProductCatalogSubscription</param>
        /// <param name="e">ProductCatalogUpdatedEventArgs</param>
        private void productsUpdated(object sender, ProductCatalogUpdatedEventArgs e)
        {
            var sub = (ProductCatalogSubscription)sender;

            List<ProductType> selectedTypes = BuildSelectedTypes();
            IEnumerable<Product> products;
            treeViewProductList.Nodes.Clear();

            // If a search filter is set filter the product list by that filter.
            // If no search filter is set list all products for the selected market.
            if (String.IsNullOrEmpty(m_searchFilter))
            {
                // Please refer to MSDN for more information about Lambda expressions used to Query collections. http://msdn.microsoft.com/en-us/library/bb397675
                products = from Product product in sub.Products.Values
                           join type in selectedTypes on product.Type equals type
                           orderby product.Name
                           select product;
            }
            else
            {
                // Please refer to MSDN for more information about Lambda expressions used to Query collections. http://msdn.microsoft.com/en-us/library/bb397675
                products = from Product product in sub.Products.Values
                           join type in selectedTypes on product.Type equals type
                           where product.Name.Contains(m_searchFilter)
                           orderby product.Name
                           select product;
            }

            // Add product nodes to the TreeView.
            // Set the default child node as "loading..." This node will be replaced when the node is expanded and
            // the instrument catalog is downloaded for the given product.
            foreach (Product product in products)
            {
                TreeNode prodNode = treeViewProductList.Nodes.Add(product.Name);
                prodNode.Tag = (object)product;
                prodNode.SelectedImageIndex = prodNode.ImageIndex = GetImageCode(product.Type);

                TreeNode childNode = new TreeNode("Loading ...", GetImageCode(product.Type), GetImageCode(product.Type));
                prodNode.Nodes.Add(childNode);
            }

            // Clean up this ProductCatalog subscription.
            if (m_prodCat != null)
            {
                m_prodCat.ProductsUpdated -= new EventHandler<ProductCatalogUpdatedEventArgs>(productsUpdated);
                m_prodCat.Dispose();
                m_prodCat = null;
            }

            toolStripStatusLabel.Text = "There are " + Convert.ToString(products.Count()) + " products";
            Cursor = Cursors.Default;
        }

        #endregion

        #region InstrumentCatalogSubscription

        /// <summary>
        /// BeforeExpand event of the product list tree view.
        /// </summary>
        private void treeViewProductList_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeViewProductList.SelectedNode = e.Node;
            Product product = e.Node.Tag as Product;

            subscribeToInstrumentCatalog(product);
        }

        /// <summary>
        /// Subscribe to the instrument catalog for a given product.
        /// </summary>
        /// <param name="product">Product to subscribe to.</param>
        private void subscribeToInstrumentCatalog(Product product)
        {
            if (!m_instrumentCatalogSubscriptionList.ContainsKey(product))
            {
                // Create and start an instrument catalog subscription.
                InstrumentCatalogSubscription instrumentCatalogSubscription = new InstrumentCatalogSubscription(product, m_dispatcher);
                instrumentCatalogSubscription.InstrumentsUpdated += new EventHandler<InstrumentCatalogUpdatedEventArgs>(instrumentsUpdated);
                instrumentCatalogSubscription.Start();

                // Track this subscription for cleanup.
                m_instrumentCatalogSubscriptionList.Add(product, instrumentCatalogSubscription);
            }
            else
            {
                // The instrument subscription was already made. Update the view.
                updateInstrumentTreeViewNodes(m_instrumentCatalogSubscriptionList[product]);
            }
        }

        /// <summary>
        /// InstrumentCatalogSubscription InstrumentsUpdated event.
        /// </summary>
        private void instrumentsUpdated(object sender, InstrumentCatalogUpdatedEventArgs e)
        {
            InstrumentCatalogSubscription instrumentCatalogSubscription = sender as InstrumentCatalogSubscription;
            updateInstrumentTreeViewNodes(instrumentCatalogSubscription);
        }

        /// <summary>
        /// Update the instrument tree view nodes for a given product.
        /// </summary>
        /// <param name="instrumentCatalogSubscription">InstrumentCatalogSubscription</param>
        private void updateInstrumentTreeViewNodes(InstrumentCatalogSubscription instrumentCatalogSubscription)
        {
            treeViewProductList.BeginUpdate();
         
            TreeNode updatedNode = null;

            // Find the product in the tree view for the selected product tree view node.
            foreach (TreeNode node in treeViewProductList.Nodes)
            {
                if (String.Equals((node.Tag as Product).Name, instrumentCatalogSubscription.Product.Name))
                {
                    updatedNode = node;
                    break;
                }
            }

            // updatedNode should never be null.
            if (updatedNode == null)
            {
                return;
            }

            // Clear out any items currently in the node. ("Loading ...")
            updatedNode.Nodes.Clear();

            // Insert the instruments as child nodes within the product tree view node.
            foreach (Instrument instr in instrumentCatalogSubscription.Instruments.Values)
            {
                TreeNode node = updatedNode.Nodes.Add(instr.GetFormattedName(InstrumentNameFormat.Normal));
                node.ImageIndex = node.SelectedImageIndex = GetImageCode(instr.Product.Type);

                // Add tooltip text from the Definition
                node.ToolTipText = instr.GetFormattedName(InstrumentNameFormat.Short) + "\r\n"
                    + instr.InstrumentDetails.Currency + "\r\n" + instr.InstrumentDetails.TickSize.Numerator.ToString()
                    + " / " + instr.InstrumentDetails.TickSize.Numerator.ToString();

                node.Tag = instr;
            }

            Cursor = Cursors.Default;
            treeViewProductList.EndUpdate();
        }

        #endregion

        #region Drag and drop from view. (Instruments and Products)

        /// <summary>
        /// Allow drag of instrument or product from tree view.
        /// </summary>
        private void treeViewProductList_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode node = e.Item as TreeNode;
            if (node != null)
            {
                DataObject dataObject = new DataObject();

                if (node.Nodes.Count > 0)
                {
                    ProductKey key = ((Product)node.Tag).Key;
                    DoDragDrop(key.ToDataObject(), DragDropEffects.Copy);
                }
                else
                {
                    InstrumentKey key = ((Instrument)node.Tag).Key;
                    DoDragDrop(key.ToDataObject(), DragDropEffects.Copy);
                }
            }
        }

        #endregion

        #region GUI

        /// <summary>
        /// Setup the list view windows.
        /// </summary>
        private void initWindowViews()
        {
            ColumnHeader exchangeHeader = new ColumnHeader();
            exchangeHeader.Text = "Markets";
            exchangeHeader.Width = listViewMarketList.ClientRectangle.Width;
            listViewMarketList.Columns.Add(exchangeHeader);

            ColumnHeader feedStatusHeader = new ColumnHeader();
            feedStatusHeader.Text = "Market Feed Status";
            feedStatusHeader.Width = listViewMarketFeeds.ClientRectangle.Width;
            listViewMarketFeeds.Columns.Add(feedStatusHeader);

            ColumnHeader productTypeHeader = new ColumnHeader();
            productTypeHeader.Text = "Product Types";
            productTypeHeader.Width = listViewProductTypeList.ClientRectangle.Width;
            listViewProductTypeList.Columns.Add(productTypeHeader);

            ColumnHeader productsHeader = new ColumnHeader();
            productsHeader.Text = "Products";
            productsHeader.Width = treeViewProductList.ClientRectangle.Width;
        }

        /// <summary>
        /// TextChanged event from search box.
        /// This will filter the products TreeView.
        /// </summary>
        private void textBoxSearchText_TextChanged(object sender, EventArgs e)
        {
            m_searchFilter = textBoxSearchText.Text.ToUpper();
            listViewProductTypeList_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// SelectedIndexChanged event from the market ListView window.
        /// This event fires when the user changes the selected market.
        /// </summary>
        private void listViewMarketList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewMarketList.SelectedItems.Count > 0)
            {
                treeViewProductList.Nodes.Clear();
                toolStripStatusLabel.Text = "";
                listViewProductTypeList.Items.Clear();
                listViewMarketFeeds.Groups.Clear();
                listViewMarketFeeds.Items.Clear();

                // Cleanup and dispose all feeds.
                foreach (FeedListViewItem item in m_feedList)
                {
                    item.Dispose();
                }
                m_feedList.Clear();

                MarketListViewItem marketItem = listViewMarketList.SelectedItems[0] as MarketListViewItem;

                IEnumerable<ProductType> types = marketItem.SupportedProductTypes;
                foreach (ProductType type in types)
                {
                    ListViewItem item    = new ListViewItem();
                    item.Name = type.ToString();
                    item.Text = type.ToString();
                    item.StateImageIndex = item.ImageIndex = GetImageCode(type);
                    item.Tag = type;
                    listViewProductTypeList.Items.Add(item);
                }

                if (listViewProductTypeList.Items.Count > 0)
                {
                    listViewProductTypeList.Items[0].Selected = true;
                    listViewMarketList.Select();
                }

                // Create the market subscription.
                createProductSubscription(marketItem.Market);

                var allFeeds = marketItem.Market.PriceFeeds.Values.AsEnumerable<Feed>()
                    .Concat(marketItem.Market.OrderFeeds.Values)
                    .Concat(marketItem.Market.FillFeeds.Values);

                foreach (Feed feed in allFeeds)
                {
                    FeedListViewItem feedItem = new FeedListViewItem(listViewMarketFeeds);
                    feedItem.Feed = feed;
                    m_feedList.Add(feedItem);
                }
            }
        }

        /// <summary>
        /// SelectedIndexChanged event from the product ListView window.
        /// This event fires when the user changes the selected product.
        /// </summary>
        private void listViewProductTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewMarketList.SelectedItems.Count > 0)
            {
                Cursor = Cursors.WaitCursor;
                Market market = listViewMarketList.SelectedItems[0].Tag as Market;

                if (m_prodCat != null)
                {
                    m_prodCat.ProductsUpdated -= productsUpdated;
                    m_prodCat.Dispose();
                    m_prodCat = null;
                }

                // Create the market subscription.
                createProductSubscription(market);
            }
        }

        #endregion

        #region Helper Functions

        /// <summary>
        /// Helper to determine the ProductTypes selected in the Product Type window.
        /// </summary>
        /// <returns>List of selected ProductTypes.</returns>
        private List<ProductType> BuildSelectedTypes()
        {
            var selectedTypes = new List<ProductType>() { };

            ListView.SelectedListViewItemCollection col = listViewProductTypeList.SelectedItems;
            foreach (ListViewItem item in col)
            {
                // The ProductType is stored in the item Tag.
                selectedTypes.Add( (ProductType)item.Tag );
            }

            return selectedTypes;
        }
        
        /// <summary>
        /// Translate the ProductType to an int value to be for imageListProductTypes.
        /// </summary>
        /// <param name="prodType">Type of product.</param>
        /// <returns>Image ID mapping to images in imageListProductTypes.</returns>
        private int GetImageCode(ProductType prodType)
        {
            if (prodType == ProductType.Future)                  { return 0; }
            else if (prodType == ProductType.Spread)             { return 1; }
            else if (prodType == ProductType.Strategy)           { return 2; }
            else if (prodType == ProductType.Bond)               { return 3; }
            else if (prodType == ProductType.Energy)             { return 4; }
            else if (prodType == ProductType.Forex)              { return 5; }
            else if (prodType == ProductType.Stock)              { return 6; }
            else if (prodType == ProductType.Option)             { return 7; }
            else if (prodType == ProductType.Warrant)            { return 8; }
            else if (prodType == ProductType.NDF)                { return 9; }
            else if (prodType == ProductType.Swap)               { return 10; }
            else if (prodType == ProductType.AutospreaderSpread) { return 11; }
            else
            {
                return 0;
            }
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Cleanup subscriptions before shutting down.
        /// </summary>
        private void Cleanup()
        {
            // Dispose may be called on another thread due to garbage collection.
            // InvokeIfRequired is an extension method defined in Utility.cs within this project.
            // Please refer to Utility.cs for more information on what InvokeIfRequired is doing.
            m_dispatcher.InvokeIfRequired(() =>
            {
                if (m_prodCat != null)
                {
                    m_prodCat.ProductsUpdated -= new EventHandler<ProductCatalogUpdatedEventArgs>(productsUpdated);
                    m_prodCat.Dispose();
                    m_prodCat = null;
                }

                if (m_instrumentCatalogSubscriptionList.Count > 0)
                {
                    foreach (InstrumentCatalogSubscription instrCat in m_instrumentCatalogSubscriptionList.Values)
                    {
                        instrCat.InstrumentsUpdated -= new EventHandler<InstrumentCatalogUpdatedEventArgs>(instrumentsUpdated);
                        instrCat.Dispose();
                    }
                    m_instrumentCatalogSubscriptionList.Clear();
                }

                foreach (MarketListViewItem market in m_marketList)
                {
                    market.Dispose();
                }
                m_marketList.Clear();

                foreach (MarketListViewItem market in listViewMarketList.Items)
                {
                    market.Dispose();
                }
                listViewMarketList.Items.Clear();

                foreach (FeedListViewItem feed in m_feedList)
                {
                    feed.Dispose();
                }
                m_feedList.Clear();

                foreach (FeedListViewItem feed in listViewMarketFeeds.Items)
                {
                    feed.Dispose();
                }
                listViewMarketFeeds.Items.Clear();

                // Shutdown the TT API instance.
                m_apiInstance.Shutdown();
            });
        }

        /// <summary>
        /// Dispatcher ShutdownStarted event.
        /// This is triggered when a dispatcher begins shutting down.
        /// </summary>
        void m_dispatcher_ShutdownStarted(object sender, DispatcherShutdownEventArgs e)
        {
            Cleanup();
        }

        /// <summary>
        /// Form FormClosing event.
        /// This is triggered when a form is closed or the app is shutdown.
        /// </summary>
        private void MarketExplorer2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cleanup();
        }

        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDTS aboutForm = new AboutDTS();
            aboutForm.Show();
        }
    }
}
