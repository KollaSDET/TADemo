Feature: Home
As a online buyer
I would able to locate a best available Laptop device
So that to purchase online without going to physical store

Scenario: Purchase a Product Item
	Given I have navigated to DemoBlaze home page
	And I have seen "PRODUCT STORE" as the main label
	And I have clicked on "Laptops" menu item
	Then I have clicked on "Sony vaio i7" product item
	And I have verified product item "Sony vaio i7" on product details page
	#	#| Name         | Price |
	#	#| Sony vaio i7 | $790  |
	#And I have clicked on "Add to cart" button
	#And I have accepted an alert as "Product added"