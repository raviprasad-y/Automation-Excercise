Feature: Checkout with Payment

  Background:
    Given I have added items to the cart
    And I am on the checkout page

  Scenario: Place order successfully
    When I fill shipping address with valid data
    And I proceed to payment
    And I enter credit card "4111111111111111", expiry "12/25", cvv "123"
    And I confirm order
    Then I should see "Your order has been placed successfully!"

  Scenario: Place order with invalid payment
    When I enter invalid card "1234"
    And I try to confirm order
    Then I should see validation error on payment form
