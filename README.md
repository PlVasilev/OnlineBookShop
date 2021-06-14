# OnlineBookShop

### ASP .NET 5.0 API Server &amp; Angular 12 Client

### Seller APP Idea
This is **extreamly** simple app for Managing On Line Book Store.

The owners (admin) can manage the book inventory and the information about the books. 
For the store users there are books listing, search and filtering. All users can buy books.

My goal with this app was to create functionality for about 30 h of work in 3 days. 
There was some wonder how everything should be structured and what pattern is best for the Client and the Server that took time.

The Server has implemented Swagger - I find it realy helpful tool 
The Client has implemented Toastr  - For better UX

#### Finaly the app works as intended but there are things that can be done better:
1. There are many **Magic Strings and numbers** those must go to global constants class.
2. Client Module structure and services can be splited differently now they are 
2.1 Adminstration - admin section
2.2 Core - Nav Landing Footer NotFound Unauthorized - pages
2.3 General - All User functionality
3. In Server I think now Server Inventory entity can hold all the books, maybe that could have been the smarter choise.
4. One or two Controllers on post retun 200 it should be 201.
5. I have made mistake early some of the Controller return only ActionResult, they could have retuned responce model for better testing.
6. Test needed more time for all the Edge cases to be tested, now they mostly represent - **Smoke testing** consept.

### Well I must say I learned important lessons here and next time the mistake I`ve mentioned will be dealt with.

## Run the Application

### Without Database back up

1. Download the repo https://github.com/PlVasilev/OnlineBookShop
2. in Folder OnLineBookStore.Server is ASP .NET Server
2.1. Change the DB name in connection string to your Data base 
2.2. Run the OnLineBookStore.server not IIS
2.3. Server will create Empty Database with seeded User Roles
3. In Folder Client is Anglar 12 Client
3.1. Open CMD and run **npm-install** to get all the libraries
3.1. After that run  **ng serve** and the app should be working
4. First Created User is Admin every other is simple User
5. When First user is created the Inventory Entity is created
6. When Any user is registered in the process is created a Shpoing Cart related to him

### With Database .bak file

**Load the DB bak file in the SQL Server first**
The app should have:
1. Admin user - un: admin, pass: 123  
2. User user - un: user, pass: 123  
3. 5 Books
4. Inventory Entity to store only the Quantity limit Thrashhold value 
   (below it all books are with limited quantaty, starting value: 10)
5. Every User has Shoping card One to One relation it is created on registaring

### Tests 
1. OnlineBookShop.Tests are tests only for the API
2. After running them I sugest you Delete DB and restore it again with the **.bak** file
	(while test are running DB will be populated with irrelevant data only for testing purposes)

## App functionality

#### User
1. Logged user can see all the books - books are ordered by number of purchases
2. Logged user can search books by Author and Title - searched books are ordered by number of purchases
3. Logged user can filter books by Price in range with min and max boundaries - are ordered by number of purchases
4. Logged user can see deletails for evey book
5. Logged user from Details page can add different quantaty of viewed book to his/her cart
6. Logged user form Cart page can check out the books stored in the cart
7. After every Check out data for every book which has a copy sold is updated 

#### Admin

##### All of the users functionality plus
1. Creating a book from management create book page - All field have validations
	(Book Quantity Limit is the maximum copies of the book that can be in stock. It Can NOT be less than Quantity field)
2. Check Inventory from management inventory page - 
	books there are in form of table ordered by Qnatity Ascending
	(asuming Owner will want to take action if there is no quantity of copies from a book left)
2.1. Inventory has form to chage the threshold below which the all books will be marcked with **Limited Quantity** - 10 is the first value.
2.2. From Inventory table last colum admin can update Book.
3. From Details page Admin can also go to update book page. Admin can edit some of the book fields there.
4. From Details page Admin can delete book using the button Delete.

 