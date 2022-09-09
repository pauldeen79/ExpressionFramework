Feature: SwitchExpression

A switch expression is an expression with several cases.
Each case is a set of conditions, and an expression that will be used when the conditions are true.
A switch expression optionaly has a default expression, which is returned when no case is valid.

Scenario: First case is valid
    Given I have a switch expression
    And I add the following case
        | Field                    | Value       |
        | ConditionLeftExpression  | A           |
        | ConditionOperator        | Equals      |
        | ConditionRightExpression | A           |
        | Expression               | Hello world |
    And I add the following case
        | Field                    | Value    |
        | ConditionLeftExpression  | A        |
        | ConditionOperator        | Equals   |
        | ConditionRightExpression | B        |
        | Expression               | NotValid |
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value       |
        | Status | Ok          |
        | Value  | Hello world |

Scenario: Second case is valid
    Given I have a switch expression
    And I add the following case
        | Field                    | Value    |
        | ConditionLeftExpression  | A        |
        | ConditionOperator        | Equals   |
        | ConditionRightExpression | B        |
        | Expression               | NotValid |
    And I add the following case
        | Field                    | Value       |
        | ConditionLeftExpression  | A           |
        | ConditionOperator        | Equals      |
        | ConditionRightExpression | A           |
        | Expression               | Hello world |
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value       |
        | Status | Ok          |
        | Value  | Hello world |

Scenario: No case is valid
    Given I have a switch expression
    And I add the following case
        | Field                    | Value    |
        | ConditionLeftExpression  | A        |
        | ConditionOperator        | Equals   |
        | ConditionRightExpression | B        |
        | Expression               | NotValid |
    And I add the following case
        | Field                    | Value        |
        | ConditionLeftExpression  | A            |
        | ConditionOperator        | Equals       |
        | ConditionRightExpression | C            |
        | Expression               | AlsoNotValid |
    And I set the default expression to 'Hello world'
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value       |
        | Status | Ok          |
        | Value  | Hello world |

Scenario: No cases are available
    Given I have a switch expression
    And I set the default expression to 'Hello world'
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value       |
        | Status | Ok          |
        | Value  | Hello world |

Scenario: No cases or default value are available
    Given I have a switch expression
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value |
        | Status | Ok    |
        | Value  |       |
