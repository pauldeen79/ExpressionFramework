Feature: ConditionalExpression

A conditional expression is a combination of a condition, and two expressions: one which will be used in case the condition is true, or one which will be used when the condition is false.

Scenario: Conditional expression
    Given I have the following condition
        | Field           | Value       |
        | LeftExpression  | Hello world |
        | Operator        | Equals      |
        | RightExpression | Hello world |
    And I have a conditional expression
    And I use a constant expression 'Yes' as result expression
    And I use a constant expression 'No' as default expression
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value |
        | Status | Ok    |
        | Value  | Yes   |
