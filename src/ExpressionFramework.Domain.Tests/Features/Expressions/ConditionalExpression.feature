Feature: ConditionalExpression

A conditional expression is a combination of a condition, and two expressions: one which will be used in case the condition is true, or one which will be used when the condition is false

Scenario: Condition evaluates to true
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

Scenario: Condition evaluates to false with default expression available
    Given I have the following condition
        | Field           | Value       |
        | LeftExpression  | Hello world |
        | Operator        | Equals      |
        | RightExpression | WrongValue  |
    And I have a conditional expression
    And I use a constant expression 'Yes' as result expression
    And I use a constant expression 'No' as default expression
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value |
        | Status | Ok    |
        | Value  | No    |

Scenario: Condition evaluates to false with no default expression available
    Given I have the following condition
        | Field           | Value       |
        | LeftExpression  | Hello world |
        | Operator        | Equals      |
        | RightExpression | WrongValue  |
    And I have a conditional expression
    And I use a constant expression 'Yes' as result expression
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value |
        | Status | Ok    |
        | Value  |       |

Scenario: No conditions evaluates to true
    Given I have a conditional expression
    And I use a constant expression 'Yes' as result expression
    And I use a constant expression 'No' as default expression
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value |
        | Status | Ok    |
        | Value  | Yes   |