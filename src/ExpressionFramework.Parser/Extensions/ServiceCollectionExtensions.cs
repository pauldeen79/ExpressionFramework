namespace ExpressionFramework.Parser.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddExpressionParser(this IServiceCollection services)
        => services
            .AddExpressionParsers()
            .AddSingleton<IExpressionFrameworkParser, ExpressionFrameworkParser>();
}
