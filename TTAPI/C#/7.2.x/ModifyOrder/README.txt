README.txt
ModifyOrder (C#)
===============================================================================

Overview
-------------------------------------------------------------------------------

This example demonstrates using the TT API to modify an order.  Modifications 
include change, cancel/replace, delete last order, delete all orders and delete
a specified range of orders.

Note:	Deleting all orders can include orders placed outside of the TT API 
		application.  


Instructions
-------------------------------------------------------------------------------

1. Drag and drop a contract from the X_TRADER Market Grid.
2. The instrument properties and market data will be populated and the order
   entry fields enabled.
3. To place an order select the Customer (Note: These are taken from X_TRADER 
   | Settings | Customer Defaults), enter a price and quantity and click the
   "Buy" or "Sell" button.  A limit order will be submitted and the 
   SiteOrderKey will be populated in the "Last Order" TextBox.
4. To change or cancel/replace an order, select the option in the "Modify Order"
   GroupBox.  Enter a price and quantity, and click the "Invoke" button.
5. To delete an order, select one of the delete buttons in the "Delete Order"
   GroupBox.
   

TT API Objects
-------------------------------------------------------------------------------

CustomerDefaultsSubscription
CustomerDefaultEntry
InstrumentTradeSubscription
OrderProfile
OrderFeed


Revisions
-------------------------------------------------------------------------------

Version:		1.0.0
Date Created:	05/15/2012
Notes:			None

Version:		1.1.0
Date Created:	01/18/2013
Notes:			Updated for GitHub.  