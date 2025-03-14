namespace ExpressionFramework.Core.Tests.Evaluatables;

public class ComposedEvaluatableTests : TestBase<ComposedEvaluatableBuilder>
{
    public class Evaluate : ComposedEvaluatableTests
    {
        [Fact]
        public void Returns_Correct_Result_On_Simple_Conditions_All_True()
        {
            // Arrange
            var sut = CreateSut()
                .AddConditions(
                    new ComposableEvaluatableBuilder().WithInnerEvaluatable(new ConstantEvaluatableBuilder().WithValue(true)),
                    new ComposableEvaluatableBuilder().WithInnerEvaluatable(new ConstantEvaluatableBuilder().WithValue(true)),
                    new ComposableEvaluatableBuilder().WithInnerEvaluatable(new ConstantEvaluatableBuilder().WithValue(true)))
                .BuildTyped();

            // Act
            var result = sut.Evaluate(null);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }

        [Fact]
        public void Returns_Correct_Result_On_Simple_Conditions_One_False()
        {
            // Arrange
            var sut = CreateSut()
                .AddConditions(
                    new ComposableEvaluatableBuilder().WithInnerEvaluatable(new ConstantEvaluatableBuilder().WithValue(true)),
                    new ComposableEvaluatableBuilder().WithInnerEvaluatable(new ConstantEvaluatableBuilder().WithValue(false)),
                    new ComposableEvaluatableBuilder().WithInnerEvaluatable(new ConstantEvaluatableBuilder().WithValue(true)))
                .BuildTyped();

            // Act
            var result = sut.Evaluate(null);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(false);
        }

        [Fact]
        public void Returns_Correct_Result_On_Simple_Conditions_One_Not_Successful()
        {
            // Arrange
            var sut = CreateSut()
                .AddConditions(
                    new ComposableEvaluatableBuilder().WithInnerEvaluatable(new ConstantEvaluatableBuilder().WithValue(true)),
                    new ComposableEvaluatableBuilder().WithInnerEvaluatable(new ConstantResultEvaluatableBuilder().WithResult(Result.Error<bool>("Kaboom"))),
                    new ComposableEvaluatableBuilder().WithInnerEvaluatable(new ConstantEvaluatableBuilder().WithValue(true)))
                .BuildTyped();

            // Act
            var result = sut.Evaluate(null);

            // Assert
            result.Status.ShouldBe(ResultStatus.Error);
            result.ErrorMessage.ShouldBe("Kaboom");
        }

        [Fact]
        public void Returns_Correct_Result_On_Complex_Conditions_All_True()
        {
            // Arrange
            var sut = CreateSut()
                .AddConditions(
                    new ComposableEvaluatableBuilder().WithStartGroup().WithInnerEvaluatable(new ConstantEvaluatableBuilder().WithValue(true)),
                    new ComposableEvaluatableBuilder().WithInnerEvaluatable(new ConstantEvaluatableBuilder().WithValue(true)).WithCombination(Domains.Combination.Or).WithEndGroup())
                .BuildTyped();

            // Act
            var result = sut.Evaluate(null);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }

        [Fact]
        public void Returns_Correct_Result_On_Complex_Conditions_One_False()
        {
            // Arrange
            var sut = CreateSut()
                .AddConditions(
                    new ComposableEvaluatableBuilder().WithStartGroup().WithInnerEvaluatable(new ConstantEvaluatableBuilder().WithValue(true)),
                    new ComposableEvaluatableBuilder().WithInnerEvaluatable(new ConstantEvaluatableBuilder().WithValue(false)).WithEndGroup())
                .BuildTyped();

            // Act
            var result = sut.Evaluate(null);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(false);
        }

        [Fact]
        public void Returns_Correct_Result_On_Complex_Conditions_One_Not_Successful()
        {
            // Arrange
            var sut = CreateSut()
                .AddConditions(
                    new ComposableEvaluatableBuilder().WithStartGroup().WithInnerEvaluatable(new ConstantEvaluatableBuilder().WithValue(true)),
                    new ComposableEvaluatableBuilder().WithInnerEvaluatable(new ConstantResultEvaluatableBuilder().WithResult(Result.Error<bool>("Kaboom"))),
                    new ComposableEvaluatableBuilder().WithInnerEvaluatable(new ConstantEvaluatableBuilder().WithValue(true)).WithEndGroup())
                .BuildTyped();

            // Act
            var result = sut.Evaluate(null);

            // Assert
            result.Status.ShouldBe(ResultStatus.Error);
            result.ErrorMessage.ShouldBe("Kaboom");
        }
    }
}
