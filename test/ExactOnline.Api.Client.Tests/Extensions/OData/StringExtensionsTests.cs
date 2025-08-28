using ExactOnline.Api.Client.Extensions;

namespace ExactOnline.Api.Client.Tests.Extensions.OData;

public class StringExtensionsTests
{
    [Test]
    [Arguments("O'Reilly", "'O%27%27Reilly'")]
    [Arguments("test space", "'test%20space'")]
    [Arguments("test\"x", "'test%22x'")]
    [Arguments("test#x", "'test%23x'")]
    [Arguments("test%x", "'test%25x'")]
    [Arguments("test&x", "'test%26x'")]
    [Arguments("test+x", "'test%2Bx'")]
    [Arguments("test/x", "'test%2Fx'")]
    [Arguments("test:x", "'test%3Ax'")]
    [Arguments("test;x", "'test%3Bx'")]
    [Arguments("test<x", "'test%3Cx'")]
    [Arguments("test=x", "'test%3Dx'")]
    [Arguments("test>x", "'test%3Ex'")]
    [Arguments("test?x", "'test%3Fx'")]
    [Arguments("test@x", "'test%40x'")]
    [Arguments("test[x", "'test%5Bx'")]
    [Arguments("test]x", "'test%5Dx'")]
    [Arguments(null, "null")]
    [Arguments("", "''")]
    [Arguments("plain", "'plain'")]
    public async Task ToODataFormat_ReturnsCorrectlyFormattedString(string? input, string expected)
    {
        // Act
        var result = input.ToODataFormat();

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }
}