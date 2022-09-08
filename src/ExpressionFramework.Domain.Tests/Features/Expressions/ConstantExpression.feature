Feature: ConstantExpression

A constant expression is a value that is provided by the creator, and doesn't change.

Scenario: Constant expression
    Given I have the constant expression 'Hello world'
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value       |
        | Status | Ok          |
        | Value  | Hello world |
