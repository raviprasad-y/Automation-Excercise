Feature: Product Search and Detail View

  Background:
    Given user navigate to the login page
	When user enters valid credentials
	And user click the login button
    And user on the products page

  Scenario: Search for an existing product
    When user search for "Blue Top"
    Then user should see product "Blue Top" in the results

  Scenario: View product details from listing
    When user search for "Men Tshirt"
    And user click on the product "Men Tshirt"
    Then user should be on the product detail page for "Men Tshirt"
    And the product price and description should be displayed
