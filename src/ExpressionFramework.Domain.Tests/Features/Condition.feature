﻿Feature: Condition

A condition is something that can is either true or false

Scenario: Same expressions with Equals operator should result in true
    Given I have the following condition
        | Field           | Value       |
        | LeftExpression  | Hello world |
        | Operator        | Equals      |
        | RightExpression | Hello world |
    When I evaluate the condition
    Then the condition evaluation result should contain the content
        | Field  | Value |
        | Status | Ok    |
        | Value  | true  |

Scenario: Different expressions with Equals operator should result in false
    Given I have the following condition
        | Field           | Value          |
        | LeftExpression  | Hello world    |
        | Operator        | Equals         |
        | RightExpression | Something else |
    When I evaluate the condition
    Then the condition evaluation result should contain the content
        | Field  | Value |
        | Status | Ok    |
        | Value  | false |

Scenario: Unknown operator should result in error
    Given I have the following condition
        | Field           | Value       |
        | LeftExpression  | Hello world |
        | Operator        | Unknown     |
        | RightExpression | Hello world |
    When I evaluate the condition
    Then the condition evaluation result should contain the content
        | Field  | Value   |
        | Status | Invalid |