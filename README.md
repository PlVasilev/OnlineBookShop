# OnlineBookShop

### ASP .NET 5.0 API Server &amp; Angular 12 Client

### Seller APP Idea
This is **extremely** simple app for Managing On-Line Book Store.

The owners (admin) can manage the book inventory and the information about the books. 
For the store users, there are book listing, search, and filtering. All users can buy books.

My goal with this app was to create functionality for about 30 h of work in 3 days. 
There was some wonder how everything should be structured and what pattern is best for the Client and the Server that took time.

The Server has implemented Swagger - I find it really helpful tool 
The Client has implemented Toastr  - For better UX

#### Finally the app works as intended but there are things that can be done better:
1. There are many **Magic Strings and numbers** those must go to global constants class.
2. Client Module structure and services can be split differently now they are 
2.1 Administration - the admin section
2.2 Core - Nav Landing Footer NotFound Unauthorized - pages
2.3 General - All User functionality
3. In Server I think now Server Inventory entity can hold all the books, maybe that could have been the smarter choice.
4. One or two Controllers on post, return 200 should be 201.
5. I have made mistake early some of the Controllers return only ActionResult, they could have returned response model for better testing.
6. Test needed more time for all the Edge cases to be tested, now they mostly represent - **Smoke testing** concept.

### Well I must say I learned important lessons here and next time the mistake I`ve mentioned will be dealt with.

## Run the Application

### Without Database backup

1. Download the repo https://github.com/PlVasilev/OnlineBookShop
2. in Folder OnLineBookStore.Server is ASP .NET Server
2.1. Change the DB name in the connection string to your Database 
2.2. Run the OnLineBookStore.server not IIS
2.3. Server will create Empty Database with seeded User Roles
3. In Folder Client is Angular 12 Client
3.1. Open CMD and run **npm-install** to get all the libraries
3.1. After that run  **ng serve** and the app should be working
4. First Created User is Admin every other is simple User
5. When the First user is created the Inventory Entity is created
6. When Any user is registered in the process is created a Shopping Cart related to him

### With Database .bak file

**Load the DB bak file in the SQL Server first**
The app should have:
1. Admin user - un: admin, pass: 123  
2. User user - un: user, pass: 123  
3. 5 Books
4. Inventory Entity to store only the Quantity limit Threshold value 
   (below it all books are with limited quantity, starting value: 10)
5. Every User has a Shopping card One to One relation it is created on registering

### Tests 
1. OnlineBookShop.Tests are tests only for the API
2. After running them I suggest you Delete DB and restore it again with the **.bak** file
	(while test are running DB will be populated with irrelevant data only for testing purposes)

## App functionality

#### User
1. Logged user can see all the books - books are ordered by number of purchases
2. Logged user can search books by Author and Title - searched books are ordered by number of purchases
3. Logged user can filter books by Price in range with min and max boundaries - are ordered by number of purchases
4. Logged user can see details for every book
5. Logged user from the Details page can add a different quantity of the viewed book to his/her cart
6. Logged user form Cart page can check out the books stored in the cart
7. After every Checkout data for every book which has a copy sold is updated 

#### Admin

##### All of the user's functionality plus
1. Creating a book from management create book page - All field have validations
	(Book Quantity Limit is the maximum number of copies of the book that can be in stock. It Can NOT be less than Quantity field)
2. Check Inventory from management inventory page - 
	books there are in form of a table ordered by Qnatity Ascending
	(assuming Owner will want to take action if there is no quantity of copies from a book left)
2.1. Inventory has form to change the threshold below which all books will be marked with **Limited Quantity** - 10 is the first value.
2.2. From the Inventory table last column admin can update Book.
3. From the Details page Admin can also go to the update book page. Admin can edit some of the book fields there.
4. From the Details page Admin can delete the book using the button Delete.

 