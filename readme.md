# BUYNOW - online food shop


Screenshot:
![Screenshot_2](https://user-images.githubusercontent.com/78071146/235366534-aefedd64-0b32-48d3-9290-9d26b011b69c.jpg)


## Bugs


### [Bug 1] On "Home" page all of the product names seem to be confused with "Category" names

Please display Product names instead of Category names on Home page.

### [Bug 2] On "Home" search seems to be case sensitive

Please, use case insensitive search on Home page. 
Currently if you put in Search Input "Chicken" -> Chicken will be displayed.
But if you put "chicken" -> Chicken will not be displayed. Please, fix that.

### [Bug 3] On Login page validation is broken

If user enters credentials and click submit everything is fine. But if user sends form with empty fields there is an 500 error.
URL: https://localhost:53939/Account/Login
Please, add validation to don't allow to submit form with empty fields.

### [Bug 4] Logout button seems is broken

Please fix logout button to sign out user. Now it just clears Shopping Cart.

### [Bug 7] Delete "Category" button doesn't work

Please, fix the issue with deleting categories.  

### [Bug 8] Delete "Product" button doesn't work

Please, fix the issue with deleting products.

## Tasks

### [Task 1] Change the app theme

Implement ability to change the app header theme.

#### Acceptance criteria:

There is a dropdown near to the "Contact Us" button in the Footer with 2 options:
* Light: app header has current styles;
* Dark: app header background becomes #3c3d41 and font color and icons become #ffffff;

#### Technical details:

* Use Cookies to store current theme value. The default theme is Light;

### [Task 2] Improve Header menu

When user hovers on "Categories" menu item in the Header all of the categories should appear in a dropdown for faster navigation.

### [Task 3] Keep items in cart between application runs

Currently after application restart all items from Shopping Cart dissapear.
Please, implement ability to keep Shopping Cart items between application restarts.

#### Acceptance criteria:

* Shopping Cart items are persisted between application stop/start sessions.

#### Technical details:

* Use Cookies to keep Shopping Cart items between application restart;

### [Task 4] Remove items from Shopping Cart

Currently there is no way to remove a Cart Item from the Shopping Cart. 
Implement ability to remove items from the Shopping Cart on https://localhost:53939/ShoppingCart page.

### [Task 5] Add ability to sort products by Price on Producta page

Currently when user selects category and lands on Products page there is no way to sort by any criteria.
Implement ability to sort by Price.

#### Acceptance criteria:

On top of all products there should be a dropdown with title "Sort by Price" which has 3 options:
* None - no sorting applied (Default);
* Ascending - sorting by ascending applied;
* Descending - sorting by descending applied;

When option is selected, products are display sorted accordinatelly.

### [Task 6] Add ability to filter products by Price on Producta page

Currently when user selects category and lands on Products page there is no to filter products by Price.
Implement ability to filter products by Price.

#### Acceptance criteria:

Implement 2 inputs for price range:
* Min.price - if value is provided, only products with Price > MinPrice should be displayed;
* Max.price - if value is provided, only procuts with Price <= MaxPrice should be displayed;


### [Task 7] Add ability to hide a single product on Producta page for just visitors

Currently there is no way to hide producs for visitors. As an Admin i want to have ability to create and edit products 
for future and make them visible only for regular users only when i want.
Only Admins can see hidden products.

#### Acceptance criteria:

On page https://localhost:53939/Food/Edit/{product_id} there should be a checkbox input "Is Visible" which controls visibility of the product for regular visitors.
If IsVisible is "false" the product shouldn't be visible for regular users which don't have role "Admin". Admins still see the product on the Products page.
If Product is hidden - it automatically disappers from the Shopping Cart. All related shopping cart items should be deleted.

### [Task 8] Add ability to search on Categories page

Currently there is Search functionality implemented for Products page. Our customer decided to implement similar functionality on 
Categories page. 

### [Task 9] Hide/show categories

Add ability to hide Categories using "IsVisible" property. 
The logic works similar as on Product level, but if a Category is hidden, then all of the producs are also hidden.
Only Admins can see hidden categories.

If Category is hidden - all of its Products automatically disapper from the Shopping Cart. All related shopping cart items should be deleted.

### [Task 10] Add ability to disable Cart & Checkout

There are some clients, who don't want to sell their products using our website. They just want to display their products for visitors.

#### Acceptance criteria:

Please add ability to disable Shopping Cart and Checkout functionality.

#### Technical details:

Use "appsettings.json" to create a setting "Showcase": true/false. If application is in "Showcase" mode, 
please, disable ShoppingCartController & OrderController. Also make sure that all of the Shopping Cart elements are gone from the User Interface.

For instance:
* Shopping Cart icon in the application header should disappear;
* Shopping Cart buttons and icons should disapper from a single Product pages;
* Find other places where Shopping Cart & Order should be hidden as well.

### [Task 11] Implement "Contact Us" form

There is a button "Contact Us" in the application Footer which doesn't work now. 
Please, on click show a modal window with form inside. All of the form submits should be stored in the database.

The next fields should be included:
* First Name;
* Last Name;
* Email;
* Comment;

All users with role "Admin" should have in the application Header menu new item "Contact Us". When Admin clicks on this menu item he should be redirected on a page
with list of all submitted forms sorted by Descending by CreatedDate property.

When Admin clicks on a single submission, a new modal window appears with detailed information about this "Constact Us" submission.


## License

This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/bugdaryan/FoodStore/blob/master/LICENSE) file for details

