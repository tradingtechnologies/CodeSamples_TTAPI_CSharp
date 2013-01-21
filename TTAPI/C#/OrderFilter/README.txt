README.txt
OrderFilter (C#)
===============================================================================

Overview
-------------------------------------------------------------------------------

This example demonstrates using the TT API to filter the order updates using
user defined TradeSubscriptionFilter objects. 


Instructions
-------------------------------------------------------------------------------

1. Drag and drop an Instrument from the Market Grid in X_TRADER.
2. To place an order select the Customer (Note: These are taken from X_TRADER 
   | Settings | Customer Defaults), enter a price and quantity and click the
   "Buy" or "Sell" button.  A limit order will be submitted.
3. Select multiple filters in the "Order Filters" GroupBox.  Price and Quantity
   filters must contain a value.
4. Select the appropriate boolean logic.
5. Click the "Apply" button to activate the filter.
6. Listen for updates in the "Order Add/Delete Audit Trail" GroupBox.


TT API Objects
-------------------------------------------------------------------------------

CustomerDefaultsSubscription
InstrumentTradeSubscription
TradeSubscriptionFilter
TradeSubscriptionAndFilter
TradeSubscriptionOrFilter


Revisions
-------------------------------------------------------------------------------

Version:		1.0.0
Date Created:	05/15/2012
Notes:			None

Version:		1.1.0
Date Created:	01/18/2013
Notes:			Updated for GitHub.  