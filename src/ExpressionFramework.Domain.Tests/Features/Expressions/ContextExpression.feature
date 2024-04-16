Feature: ContextExpression

A context expression is the context of the expression evaluation request

Scenario: Context expression
    Given I set the context to 'Hello world'
    And I have a context expression
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value       |
        | Status | Ok          |
        | Value  | Hello world |
