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
        private FillsSubscription m_fillSubscription = null;
        private bool m_isShutdown = false, m_shutdownInProcess = false;

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

                UpdateStatusBar("Total Fill Count: " + m_FillCount);

                // Create the Fills Subscription
                m_fillSubscription = new FillsSubscription(m_TTAPI.Session, Dispatcher.Current);

                // Subscribe to all Fill Events
                m_fillSubscription.FillListEnd += new EventHandler<FillListEventArgs>(m_FillSubscription_FillListEnd);
                m_fillSubscription.FillListStart += new EventHandler<FillListEventArgs>(m_FillSubscription_FillListStart);
                m_fillSubscription.FillBookDownload += new EventHandler<FillBookDownloadEventArgs>(m_FillSubscription_FillBookDownload);
                m_fillSubscription.FillAdded += new EventHandler<FillAddedEventArgs>(m_FillSubscription_FillAdded);
                m_fillSubscription.FillDeleted += new EventHandler<FillDeletedEventArgs>(m_FillSubscription_FillDeleted);
                m_fillSubscription.FillAmended += new EventHandler<FillAmendedEventArgs>(m_FillSubscription_FillAmended);

                // Start the subscription
                m_fillSubscription.Start();
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
                if (m_fillSubscription != null)
                {
                    m_fillSubscription.FillListEnd -= m_FillSubscription_FillListEnd;
                    m_fillSubscription.FillListStart -= m_FillSubscription_FillListStart;
                    m_fillSubscription.FillBookDownload -= m_FillSubscription_FillBookDownload;
                    m_fillSubscription.FillAdded -= m_FillSubscription_FillAdded;
                    m_fillSubscription.FillDeleted -= m_FillSubscription_FillDeleted;
                    m_fillSubscription.FillAmended -= m_FillSubscription_FillAmended;
                    m_fillSubscription.Dispose();
                    m_fillSubscription = null;
                }

                TTAPI.ShutdownCompleted += new EventHandler(TTAPI_ShutdownCompleted);
                TTAPI.Shutdown();
            }

            // only run shutdown once
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
