Feature: Shopping Cart Operations

  Background:
    Given I am logged in as "testuser"
    And I have added "Blue Top" to my cart

  Scenario: Update product quantity in cart
    When I navigate to the cart page
    And I change the quantity of "Blue Top" to 3
    Then the cart should show quantity 3
    And the total price should update accordingly

  Scenario: Remove product from cart
    When I remove "Blue Top" from cart
    Then I should see the cart is empty
