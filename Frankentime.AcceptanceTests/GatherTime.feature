Feature: GatherTime
	As a Hobart, I want to track time worked so that I can know what to bill my client at the end of the day.

@mytag
Scenario: Triggering start mechanism that should begin gathering seconds 
	Given I have triggered the start mechanism
	When 128 minutes have elapsed
	Then the result should be 2 hours and 8 minutes on the screen
