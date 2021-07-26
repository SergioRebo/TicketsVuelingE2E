Feature: Basic
	1 adult buying 1 ticket

@mytag
Scenario: 1 adult buying 1 ticket
	Given the origin Barcelona
	And the destiny Brussels
	When the ticket is bought
	Then it should redirect you to the schedule window