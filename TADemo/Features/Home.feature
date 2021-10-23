Feature: Home


Scenario: Purchase Device
	Given I have navigated to DemoBlaze home page
	And I have seen "PRODUCT STORE" as the main label 
	And I have clicked on "Laptops" menu item
	Then I have clicked on "Sony vaio i7" product item
	Then I have seen below product details
	| Name |Price|
	|	6   |	 |


	