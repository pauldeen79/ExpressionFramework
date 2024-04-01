namespace ExpressionFramework.Domain2.Tests.Builders.Expressions;

public class StringConcatenateExpressionBuilderTests
{
    protected StringConcatenateExpressionBuilder CreateSut() => new StringConcatenateExpressionBuilder();

    public class AddExpressions : StringConcatenateExpressionBuilderTests
    {
        [Fact]
        public void Can_Add_Strings_Instead_Of_TypedExpressions()
        {
            // Arrange
            var sut = CreateSut();
            TypedConstantExpressionBuilder<string> expression = "Hello world!";

            // Act
            var result = sut.AddExpressions(expression);

            // Assert
            result.Expressions.Should().ContainSingle();
        }

        [Fact]
        public void Can_Add_Func_Strings_Instead_Of_TypedExpressions()
        {
            // Arrange
            var sut = CreateSut();
            TypedDelegateExpressionBuilder<string> expression = new Func<object?, string>(_ => "Hello world!");

            // Act
            var result = sut.AddExpressions(expression);

            // Assert
            result.Expressions.Should().ContainSingle();
        }
    }
}
