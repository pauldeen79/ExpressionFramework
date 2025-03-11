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
}
