Feature: Expression

An Expression is something that can be evaluated to a value

Scenario: Unknown expression
    Given I have an unknown expression
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value   |
        | Status | Invalid |