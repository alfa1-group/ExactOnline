using ExactOnline.Api.Client.Constants;
using ExactOnline.Api.Client.Models;

namespace ExactOnline.Api.Client.Tests.Constants;

public class ConstantsTests
{
    [Test]
    public async Task DeletedEntityType_TryParse_Known_String()
    {
        var result = DeletedEntityType.TryParse("SyncPayrollEmployees", out var entityType);

        await Assert.That(result).IsTrue();
        await Assert.That(entityType).IsEqualTo(42);
    }

    [Test]
    public async Task DeletedEntityType_TryParse_Unknown_String()
    {
        var result = DeletedEntityType.TryParse("test", out var entityType);

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task DeletedEntityType_TryParse_Known_Type()
    {
        var result = DeletedEntityType.TryParse<SyncCRMAddress>(out var entityType);

        await Assert.That(result).IsTrue();
        await Assert.That(entityType).IsEqualTo(3);
    }

    [Test]
    public async Task DeletedEntityType_TryParse_Unknown_Type()
    {
        var result = DeletedEntityType.TryParse<SystemSystemDivision>(out var entityType);

        await Assert.That(result).IsFalse();
    }
}