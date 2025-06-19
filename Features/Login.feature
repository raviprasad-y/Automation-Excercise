Feature: Login Functionality

Scenario: Validating the Login functionality of Automation Excersice Application
	Given user navigate to the login page
	When user enters valid credentials
	And user click the login button
	#Then user should be logged in successfully

Scenario Outline: Unsuccessful login with invalid credentials
    When user navigate to the login page
    And user enter email "<email>" and password "<password>"
    And user click the login button
    Then user should see error message "<errorMessage>"

    Examples:
      | email              | password   | errorMessage                              |
      | wrong@example.com  | wrongpass  | Your email or password is incorrect!      |
