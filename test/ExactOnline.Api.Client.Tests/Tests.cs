namespace ExactOnline.Api.Client.Tests;

public class Tests
{
    [Test]
    public void Basic()
    {
        Console.WriteLine("This is a basic test");
    }

    [Test]
    [Arguments(1, 2, 3)]
    [Arguments(2, 3, 5)]
    public async Task DataDrivenArguments(int a, int b, int c)
    {
        Console.WriteLine("This one can accept arguments from an attribute");

        var result = a + b;

        await Assert.That(result).IsEqualTo(c);
    }
}