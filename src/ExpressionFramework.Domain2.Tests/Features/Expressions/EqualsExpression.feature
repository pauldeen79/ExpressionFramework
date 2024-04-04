Feature: EqualsExpression

The equals expression determines whether two expressions are equal.

Scenario: Two equal expressions result in true
    Given I have the equals expression with first value 'Hello world' and second value 'Hello world'
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value |
        | Status | Ok    |
        | Value  | true  |

Scenario: Two unequal expressions result in false
    Given I have the equals expression with first value 'Hello world' and second value 'something else'
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value |
        | Status | Ok    |
        | Value  | false |
