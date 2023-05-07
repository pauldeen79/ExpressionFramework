namespace ExpressionFramework.Domain.Tests;

public class SystemTests
{
    [Fact]
    public void Can_Generate_LineCount_Summary_Per_Project()
    {
        // Arrange
        var currentDirectory = Directory.GetCurrentDirectory();
        var basePath = currentDirectory switch
        {
            var x when x.EndsWith("ExpressionFramework") => Path.Combine(currentDirectory, @"src/"),
            _ => Path.Combine(currentDirectory, @"../../../../")
        };

        // Act
        var list = Directory.EnumerateDirectories(basePath)
            .Select(x => new
            {
                Name = x.Split('/').Last(),
                LineCountGenerated = ComputeLineCount(x, true),
                LineCountNotGenerated = ComputeLineCount(x, false),
                LineCountTotal = ComputeLineCount(x, null)
            })
            .Select(x => $"{x.Name},{x.LineCountGenerated},{x.LineCountNotGenerated},{x.LineCountTotal}")
            .ToArray();

        // Assert
        list.Should().NotBeEmpty();
    }

    private static long ComputeLineCount(string directory, bool? generated)
        => Directory.GetFiles(directory, "*.cs", SearchOption.AllDirectories)
            .Where(x => IsGeneratedValid(x, generated))
            .Select(x => File.ReadAllLines(x).Length)
            .Sum();

    private static bool IsGeneratedValid(string fileName, bool? generated)
        => generated switch
        {
            true => fileName.EndsWith(".template.generated.cs"),
            false => !fileName.EndsWith(".template.generated.cs"),
            null => true
        };
}
