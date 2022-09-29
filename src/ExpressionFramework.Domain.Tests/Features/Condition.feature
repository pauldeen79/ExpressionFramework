Feature: Condition

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

Scenario Outline: Condition evaluation
    Given I have the following condition
        | Field           | Value      |
        | LeftExpression  | <left>     |
        | Operator        | <operator> |
        | RightExpression | <right>    |
    When I evaluate the condition
    Then the condition evaluation result should contain the content
        | Field  | Value    |
        | Status | Ok       |
        | Value  | <result> |

Examples:
    | left    | right  | operator              | result |
    | Pizza   | x      | Contains              | False  |
    | Pizza   | a      | Contains              | True   |
    | Pizza   | A      | Contains              | True   |
    | Pizza   | a      | EndsWith              | True   |
    | Pizza   | x      | EndsWith              | False  |
    | Pizza   | A      | EndsWith              | True   |
    | [null]  | [null] | Equals                | True   |
    | True    |        | Equals                | False  |
    | A       | A      | Equals                | True   |
    | [null]  | b      | Equals                | False  |
    | [1]     | [1]    | Equals                | True   |
    | True    | True   | Equals                | True   |
    | A       | [null] | Equals                | False  |
    |         |        | Equals                | True   |
    | True    | False  | Equals                | False  |
    | [1]     | [2]    | Equals                | False  |
    | True    | [1]    | Equals                | False  |
    | A       | a      | Equals                | True   |
    | A       | b      | Equals                | False  |
    | [3]     | [2]    | IsGreater             | True   |
    | [1]     | [2]    | IsGreater             | False  |
    | [null]  | [2]    | IsGreater             | False  |
    | [2]     | [2]    | IsGreater             | False  |
    | [2]     | [null] | IsGreater             | False  |
    | [2]     | [2]    | IsGreaterOrEqual      | True   |
    | [3]     | [2]    | IsGreaterOrEqual      | True   |
    | [1]     | [null] | IsGreaterOrEqual      | False  |
    | [1]     | [2]    | IsGreaterOrEqual      | False  |
    | [null]  | [2]    | IsGreaterOrEqual      | False  |
    |         | [null] | IsNotNull             | True   |
    | A       | [null] | IsNotNull             | True   |
    | [null]  | [null] | IsNotNull             | False  |
    | [space] | [null] | IsNotNullOrEmpty      | True   |
    | [null]  | [null] | IsNotNullOrEmpty      | False  |
    | A       | [null] | IsNotNullOrEmpty      | True   |
    |         | [null] | IsNotNullOrEmpty      | False  |
    | [space] | [null] | IsNotNullOrWhiteSpace | False  |
    | [null]  | [null] | IsNotNullOrWhiteSpace | False  |
    | A       | [null] | IsNotNullOrWhiteSpace | True   |
    |         | [null] | IsNotNullOrWhiteSpace | False  |
    | A       | [null] | IsNull                | False  |
    |         | [null] | IsNull                | False  |
    | [null]  | [null] | IsNull                | True   |
    | [space] | [null] | IsNullOrEmpty         | False  |
    | A       | [null] | IsNullOrEmpty         | False  |
    |         | [null] | IsNullOrEmpty         | True   |
    | [null]  | [null] | IsNullOrEmpty         | True   |
    | [null]  | [null] | IsNullOrWhiteSpace    | True   |
    | [space] | [null] | IsNullOrWhiteSpace    | True   |
    | A       | [null] | IsNullOrWhiteSpace    | False  |
    |         | [null] | IsNullOrWhiteSpace    | True   |
    | [2]     | [3]    | IsSmaller             | True   |
    | [2]     | [1]    | IsSmaller             | False  |
    | [2]     | [2]    | IsSmaller             | False  |
    | [2]     | [null] | IsSmaller             | False  |
    | [null]  | [1]    | IsSmaller             | False  |
    | [null]  | [1]    | IsSmallerOrEqual      | False  |
    | [2]     | [null] | IsSmallerOrEqual      | False  |
    | [2]     | [3]    | IsSmallerOrEqual      | True   |
    | [2]     | [1]    | IsSmallerOrEqual      | False  |
    | [2]     | [2]    | IsSmallerOrEqual      | True   |
    | Pizza   | a      | NotContains           | False  |
    | Pizza   | A      | NotContains           | False  |
    | Pizza   | x      | NotContains           | True   |
    | Pizza   | x      | NotEndsWith           | True   |
    | Pizza   | A      | NotEndsWith           | False  |
    | Pizza   | a      | NotEndsWith           | False  |
    | A       | A      | NotEquals             | False  |
    | A       | [null] | NotEquals             | True   |
    | [null]  | b      | NotEquals             | True   |
    | A       | a      | NotEquals             | False  |
    | A       | b      | NotEquals             | True   |
    | Pizza   | P      | NotStartsWith         | False  |
    | Pizza   | x      | NotStartsWith         | True   |
    | Pizza   | p      | NotStartsWith         | False  |
    | Pizza   | p      | StartsWith            | True   |
    | Pizza   | x      | StartsWith            | False  |
    | Pizza   | P      | StartsWith            | True   |
