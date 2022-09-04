Feature: Expression

An Expression is a representation of something that can be evaluated

Scenario: Constant expression
	Given I have the expression 'Hello world'
	When I evaluate the expression
	Then the expression result should be 'Hello world'

Scenario: Unknown expression
    Given I have an unknown expression
    When I evaluate the expression
    Then the expression result should be unsuccessful