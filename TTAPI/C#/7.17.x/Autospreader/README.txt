README.txt
Autospreader (C#)
===============================================================================

Overview
-------------------------------------------------------------------------------

This example demonstrates using the TT API to create, edit, and delete 
user defined Autospreader spread contracts.


Instructions
-------------------------------------------------------------------------------

1. Click the "New" button to create a new user-defined spread contract.
2. In the SpreadDetails form, drag contracts from the X_TRADER market grid and
   adjust the appropriate properties.
3. When complete, click the "Save" button.
4. Once an Autospreader contract is created, you can manage it through the 
   "Edit", "Rename", and "Delete" buttons.
5. When a user defined contract is ready to be published, select the server 
   and click the "Launch" button.  If successful, your new user defined 
   contract should be available on the server and accessible in X_TRADER and
   other applications.


TT API Objects
-------------------------------------------------------------------------------

AutospreaderManager
SpreadDetailSubscription
MutableSpreadDetails
MarketCatalog


Revisions
-------------------------------------------------------------------------------

Version:		1.0.0
Date Created:	05/15/2012
Notes:			None

Version:		1.1.0
Date Created:	01/21/2013
Notes:			Updated for GitHub.

Version:		7.17.0
Date Created:	10/02/2013
Notes:			Updated for TT API 7.17.  Changed initialization
				and shutdown code. 