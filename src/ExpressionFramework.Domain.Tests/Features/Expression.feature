Feature: Expression

An Expression is something that can be evaluated to a value

Scenario: Constant expression
	Given I have the constant expression 'Hello world'
	When I evaluate the expression
    Then the expression result status should be 'Ok'
	And the expression result value should be 'Hello world'

Scenario: Unknown expression
    Given I have an unknown expression
    When I evaluate the expression
    Then the expression result status should be 'Invalid'

Scenario: Empty expression
    Given I have an empty expression
    When I evaluate the expression
    Then the expression result value should be '[null]'

Scenario: Context expression
    Given I set the context to 'Hello world'
    And I have a context expression
    When I evaluate the expression
    Then the expression result status should be 'Ok'
	And the expression result value should be 'Hello world'
