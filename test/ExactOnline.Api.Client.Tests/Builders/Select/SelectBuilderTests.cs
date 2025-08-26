using ExactOnline.Api.Client.Builders.Select;
using ExactOnline.Api.Client.Models;

namespace ExactOnline.Api.Client.Tests.Builders.Select;

public class SelectBuilderTests
{
    [Test]
    public async Task Build_WithNoParameters_ReturnsAllProperties()
    {
        // Act
        var result = SelectBuilder<SyncDeleted>.Build();

        // Assert
        await Assert.That(result).IsEqualTo("DeletedBy, DeletedDate, Division, EntityKey, EntityType, ID, Timestamp");
    }

    [Test]
    public async Task Build_WithSinglePropertyExpression_ReturnsCorrectPropertyName()
    {
        // Act
        var result = SelectBuilder<CRMAccount>.Build(x => x.Name);

        // Assert
        await Assert.That(result).IsEqualTo("Name");
    }

    [Test]
    public async Task Build_WithMultiplePropertyExpressions_ReturnsCommaSeparatedPropertyNames()
    {
        // Act
        var result = SelectBuilder<CRMAccount>.Build(x => x.ID, x => x.Name);

        // Assert
        await Assert.That(result).IsEqualTo("ID, Name");
    }

    [Test]
    public async Task Build_WithAnonymousObjectExpression_ReturnsCommaSeparatedPropertyNames()
    {
        // Act
        var result = SelectBuilder<CRMAccount>.Build(x => new { x.ID, x.Name });

        // Assert
        await Assert.That(result).IsEqualTo("ID, Name");
    }

    [Test]
    public void Build_WithInvalidExpressionInAnonymousObject_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => SelectBuilder<CRMAccount>.Build(x => x.ToString()));
    }
}