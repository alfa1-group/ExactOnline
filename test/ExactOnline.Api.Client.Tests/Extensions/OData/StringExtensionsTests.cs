using ExactOnline.Api.Client.Extensions;

namespace ExactOnline.Api.Client.Tests.Extensions.OData;

public class StringExtensionsTests
{
    [Test]
    [Arguments("text", "'text'")]
    [Arguments("O'Reilly", "'O''Reilly'")]
    [Arguments("'s-Hertogenbosch", "'''s-Hertogenbosch'")]
    [Arguments(null, "null")]
    [Arguments("", "''")]
    public async Task ToODataFormat_ReturnsCorrectlyFormattedString(string? input, string expected)
    {
        // Act
        var result = input.ToODataFormat();

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }
}