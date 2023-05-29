# Simple resilient workflow management using Temporal

This project simply tries to implement a resilient workflow inside a simple playground using Temporal

The workflow contains flowing steps

* Withdraw some money from the source account
* Deposit to the destination account
* Asyncronously send an email to the both parties
