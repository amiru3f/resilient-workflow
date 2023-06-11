# Simple resilient workflow management using Temporal

This project simply tries to implement a resilient workflow inside a simple playground using Temporal

The workflow contains flowing steps in order to provide a S3 bucket with a specific name:

* Withdraw some money from the user account
* Deposit to the platform account in order to pay the infra cost
* Approve the infra request by platform guys (through a link sent to a specific Discord channel)
* Create the S3 bucket using Terraform appliance
* Asyncronously send an email to user for detailed information
