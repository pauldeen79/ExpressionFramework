Feature: ChainedExpression

A chained expression is an expression that consists of multiple child expressions, which will be sequentially called.
The result of an expression is passed to the next expression.

Scenario: Chained upper case
    Given I have a chained expression
    And I chain a constant expression 'Hello world' to it
    And I chain a to upper case expression to it
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value       |
        | Status | Ok          |
        | Value  | HELLO WORLD |

Scenario: Chained lower case
    Given I have a chained expression
    And I chain a constant expression 'Hello world' to it
    And I chain a to lower case expression to it
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value       |
        | Status | Ok          |
        | Value  | hello world |

Scenario: Chained pascal case
    Given I have a chained expression
    And I chain a constant expression 'Hello world' to it
    And I chain a to pascal case expression to it
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value       |
        | Status | Ok          |
        | Value  | Hello world |
