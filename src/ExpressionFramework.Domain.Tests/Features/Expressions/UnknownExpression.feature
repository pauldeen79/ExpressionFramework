Feature: UnknownExpression

This feature describes how unknown expressions should be handled.

Scenario: Unknown expression
    Given I have an unknown expression
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value   |
        | Status | Invalid |