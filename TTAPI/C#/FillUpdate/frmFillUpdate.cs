/***************************************************************************
 *    
 *      Copyright (c) 2012 Trading Technologies International, Inc.
 *                     All Rights Reserved Worldwide
 *
 *        * * *   S T R I C T L Y   P R O P R I E T A R Y   * * *
 *
 * WARNING:  This file is the confidential property of Trading Technologies
 * International, Inc. and is to be maintained in strict confidence.  For
 * use only by those with the express written permission and license from
 * Trading Technologies International, Inc.  Unauthorized reproduction,
 * distribution, use or disclosure of this file or any program (or document)
 * derived from it is prohibited by State and Federal law, and by local law
 * outside of the U.S. 
 *
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.WinFormsHelpers;

namespace TTAPI_Samples
{
    /// <summary>
    /// FillUpdate
    /// 
    /// This example demonstrates using the TT API to retrieve fills for the
    /// authenticated X_TRADER user.
    /// </summary>
    public partial class frmFillUpdate : Form
    {
        // Declare the TTAPI objects.
        private XTraderModeTTAPI m_TTAPI = null;
        private FillsSubscription m_FillSubscription = null;

        // Private member variables
        private int m_FillCount = 0;

        public frmFillUpdate()
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
            m_TTAPI = apiInstance;
            m_TTAPI.ConnectionStatusUpdate += ttapiInstance_ConnectionStatusUpdate;
            m_TTAPI.ConnectToXTrader();
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

                UpdateStatusBar("Total Fill Count: " + m_FillCount);

                // Create the Fills Subscription
                m_FillSubscription = new FillsSubscription(m_TTAPI.Session, Dispatcher.Current);

                // Subscribe to all Fill Events
                m_FillSubscription.FillListEnd += new EventHandler<FillListEventArgs>(m_FillSubscription_FillListEnd);
                m_FillSubscription.FillListStart += new EventHandler<FillListEventArgs>(m_FillSubscription_FillListStart);
                m_FillSubscription.FillBookDownload += new EventHandler<FillBookDownloadEventArgs>(m_FillSubscription_FillBookDownload);
                m_FillSubscription.FillAdded += new EventHandler<FillAddedEventArgs>(m_FillSubscription_FillAdded);
                m_FillSubscription.FillDeleted += new EventHandler<FillDeletedEventArgs>(m_FillSubscription_FillDeleted);
                m_FillSubscription.FillAmended += new EventHandler<FillAmendedEventArgs>(m_FillSubscription_FillAmended);

                // Start the subscription
                m_FillSubscription.Start();
            }
            else
            {
                MessageBox.Show(String.Format("ConnectionStatusUpdate: {0}", e.Status.StatusMessage));
            }
        }

        /// <summary>
        /// Adds an item to the listbox
        /// </summary>
        /// <param name="item">item to add</param>
        private void AddItemToList(string item)
        {
            this.lbNotificationList.Items.Add(item);

            // ensure the last index is visible, then deselect so it's no longer highlighted
            if (this.cboAutoScrollList.Checked)
            {
                this.lbNotificationList.SelectedIndex = this.lbNotificationList.Items.Count - 1;
                this.lbNotificationList.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Adds a TreeNode to the root of the tree
        /// </summary>
        /// <param name="node">node to add</param>
        private void AddItemToTree(TreeNode node)
        {
            this.treeFill.Nodes.Add(node);

            // ensure the last node is visible
            if (this.cboAutoScrollTree.Checked)
                this.treeFill.Nodes[this.treeFill.Nodes.Count - 1].EnsureVisible();
        }

        /// <summary>
        /// Notification that the gateway will begin downloading a list of fills.  This
        /// event is fired for every gateway the authenticated user has access to, regardless 
        /// if any fills exist.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_FillSubscription_FillListStart(object sender, FillListEventArgs e)
        {
            AddItemToList("List Start: " + e.FeedConnectionKey.GatewayKey.Name);
            AddItemToTree(new TreeNode("List: " + e.FeedConnectionKey.GatewayKey.Name));
        }

        /// <summary>
        /// Notification that the gateway has finished downloading a list of fills.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_FillSubscription_FillListEnd(object sender, FillListEventArgs e)
        {
            AddItemToList("List End: " + e.FeedConnectionKey.GatewayKey.Name);
        }

        /// <summary>
        /// A batch of fills has been received.  This call will be fired in between 
        /// a FillListStart and FillListEnd event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_FillSubscription_FillBookDownload(object sender, FillBookDownloadEventArgs e)
        {
            TreeNode node = new TreeNode("FillBookDownload");

            foreach (Fill fill in e.Fills)
            {
                string fillData = GetFillDetails(fill);
                AddItemToList("FillBookDownload: " + fillData);
                node.Nodes.Add(fillData);

                m_FillCount++;
            }

            // Add the node as a child of the previous node (which is the list start node)
            this.treeFill.Nodes[this.treeFill.Nodes.Count - 1].Nodes.Add(node);
            UpdateStatusBar("Total Fill Count: " + m_FillCount);
        }

        /// <summary>
        /// A single fill has been received.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_FillSubscription_FillAdded(object sender, FillAddedEventArgs e)
        {
            string fillDetails = GetFillDetails(e.Fill);

            AddItemToList("Added: " + fillDetails);
            AddItemToTree(new TreeNode("Added: " + fillDetails));

            m_FillCount++;
            UpdateStatusBar("Total Fill Count: " + m_FillCount);
        }

        /// <summary>
        /// A single fill has been deleted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_FillSubscription_FillDeleted(object sender, FillDeletedEventArgs e)
        {
            string fillDetails = GetFillDetails(e.Fill);

            AddItemToList("Deleted: " + fillDetails);
            AddItemToTree(new TreeNode("Deleted: " + fillDetails));

            m_FillCount--;
            UpdateStatusBar("Total Fill Count: " + m_FillCount);
        }

        /// <summary>
        /// A single fill has been updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_FillSubscription_FillAmended(object sender, FillAmendedEventArgs e)
        {
            string oldFillDetails = GetFillDetails(e.OldFill);
            string newFillDetails = GetFillDetails(e.OldFill);

            AddItemToList("Amended (Old): " + oldFillDetails);
            AddItemToList("Amended (New): " + newFillDetails);
            
            TreeNode node = new TreeNode("Amended");
            node.Nodes.Add("Old: " + oldFillDetails);
            node.Nodes.Add("New: " + newFillDetails);
            AddItemToTree(node);
        }

        /// <summary>
        /// Retrieve fill parameters from the fill object
        /// </summary>
        /// <param name="fill">fill object to query</param>
        /// <returns>string representation of set fill parameters</returns>
        private string GetFillDetails(Fill fill)
        {
            return "Fill SiteOrderKey=\"" + fill.SiteOrderKey + "\" " +
                   "Price=\"" + fill.MatchPrice + "\" " +
                   "Qty=\"" + fill.Quantity + "\" " +
                   "FillType=\"" + fill.FillType.ToString() + "\" " +
                   "BuySell=\"" + fill.BuySell.ToString() + "\" " +
                   "OrderNumber=\"" + fill.OrderNumber.ToString() + "\"";
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

        #region UpdateStatusBar

        /// <summary>
        /// Update the status bar and write the message to the console in a thread safe way.
        /// </summary>
        /// <param name="message">Message to update the status bar with.</param>
        delegate void UpdateStatusBarCallback(string message);
        public void UpdateStatusBar(string message)
        {
            if (this.InvokeRequired)
            {
                UpdateStatusBarCallback statCB = new UpdateStatusBarCallback(UpdateStatusBar);
                this.Invoke(statCB, new object[] { message });
            }
            else
            {
                // Update the status bar.
                toolStripStatusLabel1.Text = message;

                // Also write this message to the console.
                Console.WriteLine(message);
            }
        }

        #endregion
    }
}
