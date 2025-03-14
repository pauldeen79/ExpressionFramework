namespace ExpressionFramework.Core.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void Can_Build_ServiceProvider_Without_Any_Errors()
    {
        // Arrange
        var serviceCollection = new ServiceCollection().AddExpressionFramework();

        // Act & Assert
        Action a = () => _ = serviceCollection.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
        a.ShouldNotThrow();
    }

    [Theory]
    [MemberData(nameof(GetAllFunctions))]
    public void Can_Resolve_Function(Type t)
    {
        // Arrange
        var serviceCollection = new ServiceCollection().AddExpressionFramework();
        using var provider = serviceCollection.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
        using var scope = provider.CreateScope();

        // Act
        var function = scope.ServiceProvider.GetServices<IFunction>().FirstOrDefault(x => x.GetType() == t);

        // Assert
        function.ShouldNotBeNull($"Function {t.FullName} could not be resolved, did you forget to register this?");
    }

    public static TheoryData<Type> GetAllFunctions()
    {
        var data = new TheoryData<Type>();
        foreach (var t in typeof(IOperator).Assembly.GetExportedTypes().Where(x => x.GetAllInterfaces().Contains(typeof(IFunction))))
        {
            data.Add(t);
        }

        return data;
    }
}
