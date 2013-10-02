README.txt
PriceUpdateManual (C#)
===============================================================================

Overview
-------------------------------------------------------------------------------

This example demonstrates using the TT API to retrieve market data from a 
single instrument by manually specifying the contract parameters.


Instructions
-------------------------------------------------------------------------------

1. Enter the Instrument Inforamtion.

Example:
	Exchange:		CME
	Product:		ES
	ProductType:	FUTURE
	Contract:		Mar13
		
2. Click the Connect button.
3. The Instrument Market Data will be populated.


TT API Objects
-------------------------------------------------------------------------------

PriceSubscription


Revisions
-------------------------------------------------------------------------------

Version:		1.0.0
Date Created:	05/15/2012
Notes:			None

Version:		1.1.0
Date Created:	01/18/2013
Notes:			Updated for GitHub.  Renamed solution from 
				PriceUpdateManualConnection to PriceUpdateManual.

Version:		7.17.0
Date Created:	10/02/2013
Notes:			Updated for TT API 7.17.  Changed initialization
				and shutdown code.