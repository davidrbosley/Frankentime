Feature: GatherTime
	As a Hobart, I want to track time worked so that I can know what to bill my client at the end of the day.

@Timer
Scenario: Triggering start mechanism should begin gathering seconds 
	Given I have triggered the start mechanism
	When 128 minutes have elapsed
	Then the result should be 2 hours and 8 minutes on the screen

@Timer
Scenario: Triggering stop mechanism should stop the gathering of seconds 
	Given I have triggered the start mechanism
	And 69 minutes have elapsed
	When I trigger the stop mechanism
	Then the result should be 1 hours and 9 minutes on the screen
		
@Timer
Scenario: Triggering clear mechanism should flush any gathered seconds 
	Given I have 42 minutes gathered
	When I trigger the clear mechanism
	Then the result should be 0 hours and 0 minutes on the screen

@Timer
Scenario: The total number of gathered seconds should be visible in an Hour, Minute, Second format. 
	Given I have 129 minutes and 37 seconds gathered
	When I look at the screen
	Then the result should be 2 hours and 9 minutes and 37 seconds on the screen