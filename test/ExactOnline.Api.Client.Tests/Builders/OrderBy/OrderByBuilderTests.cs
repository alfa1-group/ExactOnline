using ExactOnline.Api.Client.Builders.OrderBy;
using ExactOnline.Api.Client.Models;

namespace ExactOnline.Api.Client.Tests.Builders.OrderBy;

public class OrderByBuilderTests
{
    [Test]
    public async Task OrderBy_Builds_Correct_Ascending()
    {
        // Act
        var result = OrderByBuilder<SystemSystemDivision>.OrderBy(x => x.Description).Build();

        // Assert
        await Assert.That(result).IsEqualTo("Description asc");
    }

    [Test]
    public async Task OrderByDescending_Builds_Correct_Descending()
    {
        // Act
        var result = OrderByBuilder<SystemSystemDivision>.OrderByDescending(x => x.Code).Build();

        // Assert
        await Assert.That(result).IsEqualTo("Code desc");
    }

    [Test]
    public async Task ThenBy_Chained_After_OrderBy_Builds_Correct()
    {
        // Act
        var result = OrderByBuilder<SystemSystemDivision>
            .OrderBy(x => x.Description)
            .ThenBy(x => x.Code)
            .Build();

        // Assert
        await Assert.That(result).IsEqualTo("Description asc, Code asc");
    }

    [Test]
    public async Task ThenByDescending_Chained_After_OrderBy_Builds_Correct()
    {
        // Act
        var result = OrderByBuilder<SystemSystemDivision>
            .OrderBy(x => x.Description)
            .ThenByDescending(x => x.Code)
            .Build();

        // Assert
        await Assert.That(result).IsEqualTo("Description asc, Code desc");
    }

    [Test]
    public async Task Multiple_ThenBy_And_ThenByDescending_Chain_Builds_Correct()
    {
        // Act
        var result = OrderByBuilder<SystemSystemDivision>
            .OrderByDescending(x => x.Country)
            .ThenBy(x => x.Description)
            .ThenByDescending(x => x.Code)
            .Build();

        // Assert
        await Assert.That(result).IsEqualTo("Country desc, Description asc, Code desc");
    }
}