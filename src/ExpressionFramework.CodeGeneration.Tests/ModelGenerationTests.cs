using TextTemplateTransformationFramework.Runtime;

namespace ExpressionFramework.CodeGeneration.Tests;

public class ModelGenerationTests
{
    private static readonly CodeGenerationSettings Settings = new CodeGenerationSettings
    (
        basePath: Path.Combine(Directory.GetCurrentDirectory(), @"../../../../"),
        generateMultipleFiles: true,
        dryRun: false
    );

    [Fact]
    public void Can_Generate_Everything()
    {
        Console.WriteLine("Current directory is: " + Directory.GetCurrentDirectory());
        var multipleContentBuilder = new MultipleContentBuilder(Settings.BasePath);
        GenerateCode.For<AbstractionsBuildersInterfaces>(Settings, multipleContentBuilder);
        GenerateCode.For<AbstractionsExtensionsBuilders>(Settings, multipleContentBuilder);
        GenerateCode.For<CoreEntities>(Settings, multipleContentBuilder);
        GenerateCode.For<CoreBuilders>(Settings, multipleContentBuilder);
        Verify(multipleContentBuilder);
    }

    [Fact]
    public void FixTypeName_Returns_Correct_Result_For_NonGeneric_Type()
    {
        // Arrange
        var input = typeof(int).FullName;

        // Act
        var actual = input.FixTypeName();

        // Assert
        actual.Should().Be("System.Int32");
    }

    [Fact]
    public void FixTypeName_Returns_Correct_Result_For_Nullable_Type()
    {
        // Arrange
        var input = typeof(int?).FullName;

        // Act
        var actual = input.FixTypeName();

        // Assert
        actual.Should().Be("System.Nullable<System.Int32>");
    }

    [Fact]
    public void FixTypeName_Returns_Correct_Result_For_Generic_Func()
    {
        // Arrange
        var input = typeof(Func<int>).FullName;

        // Act
        var actual = input.FixTypeName();

        // Assert
        actual.Should().Be("System.Func<System.Int32>");
    }

    [Fact]
    public void FixTypeName_Returns_Correct_Result_For_Nullable_Generic_Func()
    {
        // Arrange
        var input = typeof(Func<int?>).FullName;

        // Act
        var actual = input.FixTypeName();

        // Assert
        actual.Should().Be("System.Func<System.Nullable<System.Int32>>");
    }

    [Fact]
    public void FixTypeName_Returns_Correct_Result_For_Generic_Enumerable()
    {
        // Arrange
        var input = typeof(IEnumerable<int>).FullName;

        // Act
        var actual = input.FixTypeName();

        // Assert
        actual.Should().Be("System.Collections.Generic.IEnumerable<System.Int32>");
    }

    [Fact]
    public void FixTypeName_Returns_Correct_Result_For_Nullable_Generic_Enumerable()
    {
        // Arrange
        var input = typeof(IEnumerable<int?>).FullName;

        // Act
        var actual = input.FixTypeName();

        // Assert
        actual.Should().Be("System.Collections.Generic.IEnumerable<System.Nullable<System.Int32>>");
    }

    [Fact]
    public void FixTypeName_Returns_Correct_Result_For_Generics_With_Multiple_Generic_Parameters()
    {
        // Arrange
        var input = typeof(Func<object?, IExpression, IExpressionEvaluator, object?>).FullName;

        // Act
        var actual = input.FixTypeName();

        // Assert
        //Note that nullable generic argument types are not recognized
        actual.Should().Be("System.Func<System.Object,ExpressionFramework.CodeGeneration.Tests.ModelGenerationTests.IExpression,ExpressionFramework.CodeGeneration.Tests.ModelGenerationTests.IExpressionEvaluator,System.Object>");
    }

    private interface IExpression { }
    private interface IExpressionEvaluator { }

    private static void Verify(MultipleContentBuilder multipleContentBuilder)
    {
        var actual = multipleContentBuilder.ToString();

        // Assert
        actual.NormalizeLineEndings().Should().NotBeNullOrEmpty();
    }
}
