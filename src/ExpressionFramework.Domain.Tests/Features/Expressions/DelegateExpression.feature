Feature: DelegateExpression

A delegate expression is an expression that is dynamically evaluated. In programming languages, this is often referred as a delegate.

Scenario: Delegate expression
    Given I have the delegate expression 'Hello world'
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value       |
        | Status | Ok          |
        | Value  | Hello world |
