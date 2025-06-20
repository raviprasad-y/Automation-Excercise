Feature: Login Functionality

Scenario: Validating the Login functionality of Automation Excersice Application
	Given user navigate to the login page
	When user enters valid credentials
	And user click the login button
	#Then user should be logged in successfully

Scenario: Unsuccessful login with invalid credentials
    When user navigate to the login page
    And user enters invalid credentials
    And user click the login button
    Then user should see error message displayed

    
