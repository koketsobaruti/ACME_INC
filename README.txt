#ACME_INC
The AMCE_INC web application is an ASP.NET MVC website written in C#, Html and CSS. It makes use of MySQL Database to store
data. The application makes use of session management in order to enforce security.

##Getting Sarted
The instructions listed below will guide you on how to install and run the appliation

##What things you need to install the software and how to install them:

Any of the latest MS Visual Studio e.g. v.16.1.0 (2019)
A device with dual core processing and at least 4GB's of RAM e.g. Acer Aspire
MsSQL Server e.g. v.18.9.1
Adobe Acrobat Reader

###Installing

*Setting up the enviroment

-Open MsSQL Server and create a new database by the name 'ACME_INC_DATABASE'.
-Open the SQlScript 'AMCE_INC_TABLES' from the file and create the tables through MSSQL Server.
-Open Ms Viual Studio.
-Click on the 'Open project or solution' option and open the AMCE_INC project.
-Once open, click on the Server Explorer and select the option to connect to a new database.
-When the window pops up, add your server name from your MsSQL Server Management Studio and select the 
'ACME_INC_DATABASE'.
-Click on connect.
-To test the application click on run.

*Navigating the website

-When you have successfully launched the application, the homepage will come up.
-On this part, you can navigate the categories of products by selecting the 'Categories' option on the navigation bar or
scroll down to view a display of some of the products on offer.
-users can view more details about the products by clicking on the 'View' button.
-If the user is a customer, they can click on the login option on the navigation bar and log in. If they are unregistered, 
they can click on the "Register as customer" hyperlink to lead them to the appropriate registration page.
If they are an employee, they should click on the "Register as employee" hyperlink instead.

**CUSTOMER 
-In order to purchase items, the customer must be logged in. They can access the log in page through the navigation bar
or the login button on the View Product page.
-After successfully logging in, the customer can add items to their cart and view their cart. They can also view transaction
history by clicking on the 'Transaction History' option on the navigation bar. 
-When they click the 'Checkout' option on the 'View Cart' page, they will be propted through a series of steps to complete 
and validate their purchase.
***NB: A customer can only have one address entry and purchase one item under their account.

**EMPLOYEE
-When the employee logs on, they can view sale history by clicking on the view history page, where the purchase of each 
item by a users will be shown.
-employees can navigate through the product categories and view products as well but cannot add items to their cart or
view their cart. 

### Break down into end to end tests

To test this program:

When on the Register page, enter an email and then remove the it to test if it will highlight red.
Try to log in with invalid credentials to test if the program accepts invalid data.


## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

##Authors
**Koketso Baruti**

##Acknowledgements

* Youtube tutors