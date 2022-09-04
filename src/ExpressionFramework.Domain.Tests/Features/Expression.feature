Feature: Expression

An Expression is something that can be evaluated

Scenario: Constant expression
	Given I have the constant expression 'Hello world'
	When I evaluate the expression
    Then the result status should be 'Ok'
	And the result value should be 'Hello world'

Scenario: Unknown expression
    Given I have an unknown expression
    When I evaluate the expression
    Then the result status should be 'Invalid'

Scenario: Empty expression
    Given I have an empty expression
    When I evaluate the expression
    Then the result value should be '[null]'