Feature: User Profile and Order History

  Background:
    Given I am logged in as "testuser"

  Scenario: Edit user profile information
    When I navigate to profile page
    And I update name to "John Doe" and address to "123 Elm Street"
    Then I should see updated profile message "Profile was updated!"

  Scenario: View past orders
    When I navigate to order history
    Then I should see a list of previous orders with statuses
