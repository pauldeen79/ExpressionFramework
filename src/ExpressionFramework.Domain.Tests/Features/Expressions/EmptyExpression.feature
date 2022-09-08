Feature: EmptyExpression

An empty expression is an empty value (null / not defined)

Scenario: Empty expression
    Given I have an empty expression
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value |
        | Status | Ok    |
    And the expression evaluation result value should be '[null]'
