Feature: Expression

An Expression is something that can be evaluated to a value

Scenario: Constant expression
    Given I have the constant expression 'Hello world'
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value       |
        | Status | Ok          |
        | Value  | Hello world |

Scenario: Delegate expression
    Given I have the delegate expression 'Hello world'
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value       |
        | Status | Ok          |
        | Value  | Hello world |

Scenario: Context expression
    Given I set the context to 'Hello world'
    And I have a context expression
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value       |
        | Status | Ok          |
        | Value  | Hello world |

Scenario: Empty expression
    Given I have an empty expression
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value |
        | Status | Ok    |
    And the expression evaluation result value should be '[null]'

Scenario: Unknown expression
    Given I have an unknown expression
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value   |
        | Status | Invalid |

Scenario: Chained expression
    Given I have a chained expression
    And I chain a constant expression 'Hello world' to it
    And I chain a to upper case expression to it
    When I evaluate the expression
    Then the expression evaluation result should contain the content
        | Field  | Value       |
        | Status | Ok          |
        | Value  | HELLO WORLD |
