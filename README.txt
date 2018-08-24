Tax Figures Calculator.


A simple implementation of a Tax Transaction Data Processing System, made for a Tax Figures Calculator.

The TaxFiguresCalculator System was developed on .NET Core 2.1.

It implements foundamentals of Domain Driven Developement (DDD) as well as making use of well structure SOLID Principles.


The Database Engine is based on an Database First Approach, using Entity Framework Core.

Under DBSCript folder you can find the SQL CREATE SCHEMA SCRIPT that will generate the schema for the Database


------Table Heirarchy-----
Client 1-to->many Accounts 1-to->Many Transactions



-----Tax Figures Calculator Database Requirements-----

The 'Client Table' and 'Account' Table Must have populated values.


----Tax Figures Calculator User Expirience Requirements----

.All Transaction Tasks on the System occur on behalf of the 'Client' selected upon accesing the system.

.The user must select the Client to which he chooses to Upload/Manage Transactions
while visiting the system.

.The is a Client 


-----Upload Excel Requirements----

.The .xlxs file must have valid headers "Account" / "Description" / "Currency" / "Amount"

.The "Account" Column must correspond to existing Accounts
 on the System that belong to the specific 'Client' the user has selected for 
 uploading and managing Transactions.

.Currency Code must be of valid 42017 Currency Code

.Amount must be a valid Decimal Number

.All Fields Must be Populated

----Upon upload a user is show a summary of all the invalid transactions that have not
been up uploaded to the system-----



----Improvements & Enhancements-----

This was a shortly devised applications therefore there are certain improvements to be made:

-Adding Logging, through the .Net Core Logger API and Logger Factories for better monitoring, keeping track and maintaing the system!
-Adding Validation Alerts, alerting the user when a task has failed.
-Adding more persistent View Model Validation for better handling Model-View State.
-AntiforgeryToken Implementation on the UploadStreaming Functionality for prevention of cross-side scripting --- Implementation developed but not implemented---
-Documentation and more code commenting for understanding implementation.

-Possible Enhancements would be introducing .NET Core Identity Membership System to add login functionality


Overall it was a fun and very interesting task! It's unfortunate that recent events have really took a toll to my initial enthousiasm and 
eagerness for the overall delivery of the task but hopefully you will enjoy viewing my shortly devised software and review my implementation!!!


Best Regards,
Spyros Herodotou.
