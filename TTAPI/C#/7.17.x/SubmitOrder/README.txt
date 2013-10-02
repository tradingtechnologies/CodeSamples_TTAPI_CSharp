README.txt
SubmitOrder (C#)
===============================================================================


Overview
-------------------------------------------------------------------------------

This example demonstrates using the TT API to submit an order.  The order types
available in the application are market, limit, stop market and stop limit.  


Instructions
-------------------------------------------------------------------------------

1. Drag and drop a contract from the X_TRADER Market Grid.
2. The instrument properties and market data will be populated and the order
   entry fields enabled.
3. Select the customer the order is to be submitted under (Note: These are 
   taken from X_TRADER | Settings | Customer Defaults).
4. Select the order feed.
5. Select the order type.
6. Text boxes will be enabled based on the selected order type.
7. Enter a price, quantity and stop price if applicable.
8. Click the buy or sell button.
9. The quantity submitted will be printed in the right text box.


TT API Objects
-------------------------------------------------------------------------------

CustomerDefaultsSubscription
CustomerDefaultEntry
InstrumentTradeSubscription
PriceSubscription
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

Version:		7.17.0
Date Created:	10/02/2013
Notes:			Updated for TT API 7.17.  Changed initialization
				and shutdown code.