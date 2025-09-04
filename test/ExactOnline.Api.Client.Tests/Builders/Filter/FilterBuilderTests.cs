using System.Globalization;
using ExactOnline.Api.Client.Builders.Filter;

namespace ExactOnline.Api.Client.Tests.Builders.Filter;

public class FilterBuilderTests
{
    [Test]
    public async Task Build_With_NullableString_Equals_Expression()
    {
        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.NullableStringProperty == null);

        // Assert
        await Assert.That(result).IsEqualTo("(NullableStringProperty eq null)");
    }

    [Test]
    public async Task Build_With_String_Equals_Expression()
    {
        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.StringProperty == "test");

        // Assert
        await Assert.That(result).IsEqualTo("(StringProperty eq 'test')");
    }

    [Test]
    public async Task Build_With_String_Equals_Method_Expression()
    {
        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.StringProperty.Equals("test"));

        // Assert
        await Assert.That(result).IsEqualTo("(StringProperty eq 'test')");
    }

    [Test]
    public async Task Build_With_Bool_Equals_Expression()
    {
        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.BoolProperty == true);

        // Assert
        await Assert.That(result).IsEqualTo("(BoolProperty eq true)");
    }

    [Test]
    public async Task Build_With_Long_Equals_Expression()
    {
        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.LongProperty == 123L);

        // Assert
        await Assert.That(result).IsEqualTo("(LongProperty eq 123)");
    }

    [Test]
    public async Task Build_With_LongMaxIntValue_Equals_Expression()
    {
        // Arrange
        var value = (long)int.MaxValue + 1;

        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.LongProperty == value);

        // Assert
        await Assert.That(result).IsEqualTo("(LongProperty eq 2147483648L)");
    }

    [Test]
    public async Task Build_With_Guid_Equals_Expression()
    {
        // Arrange
        var guid = new Guid("e7d72a8b-09a1-4349-8169-63d301f120bf");

        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.GuidProperty == guid);

        // Assert
        await Assert.That(result).IsEqualTo("(GuidProperty eq guid'e7d72a8b-09a1-4349-8169-63d301f120bf')");
    }

    [Test]
    public async Task Build_With_NewGuid_Equals_Expression()
    {
        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.GuidProperty == new Guid("e7d72a8b-09a1-4349-8169-63d301f120bf"));

        // Assert
        await Assert.That(result).IsEqualTo("(GuidProperty eq guid'e7d72a8b-09a1-4349-8169-63d301f120bf')");
    }

    [Test]
    public async Task Build_With_NullableGuid_Equals_Expression()
    {
        // Arrange
        var guid = new Guid("e7d72a8b-09a1-4349-8169-63d301f120bf");

        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.NullableGuidProperty == guid);

        // Assert
        await Assert.That(result).IsEqualTo("(NullableGuidProperty eq guid'e7d72a8b-09a1-4349-8169-63d301f120bf')");
    }

    [Test]
    public async Task Build_With_NewNullableGuid_Equals_Expression()
    {
        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.NullableGuidProperty == new Guid("e7d72a8b-09a1-4349-8169-63d301f120bf"));

        // Assert
        await Assert.That(result).IsEqualTo("(NullableGuidProperty eq guid'e7d72a8b-09a1-4349-8169-63d301f120bf')");
    }

    [Test]
    public async Task Build_With_DateTimeOffset_Equals_Expression()
    {
        // Arrange
        var now = new DateTimeOffset(2023, 1, 1, 12, 0, 0, TimeSpan.Zero);

        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.DateTimeOffsetProperty == now);

        // Assert
        await Assert.That(result).IsEqualTo("(DateTimeOffsetProperty eq datetime'2023-01-01T12:00:00')");
    }

    [Test]
    public async Task Build_With_NullableDateTimeOffset_NotEquals_Expression()
    {
        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.NullableDateTimeOffsetProperty != null);

        // Assert
        await Assert.That(result).IsEqualTo("(NullableDateTimeOffsetProperty ne null)");
    }

    [Test]
    public async Task Build_With_DateTime_Equals_Expression()
    {
        // Arrange
        var now = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Utc);

        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.DateTimeProperty == now);

        // Assert
        await Assert.That(result).IsEqualTo("(DateTimeProperty eq datetime'2023-01-01T12:00:00')");
    }

    [Test]
    public async Task Build_With_Integer_GreaterThan_Expression()
    {
        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.IntProperty > 10);

        // Assert
        await Assert.That(result).IsEqualTo("(IntProperty gt 10)");
    }

    [Test]
    public async Task Build_With_Short_LessThan_Expression()
    {
        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.ShortProperty < 5000);

        // Assert
        await Assert.That(result).IsEqualTo("(ShortProperty lt 5000)");
    }

    [Test]
    public async Task Build_With_Double_LessThanOrEqual_Expression()
    {
        // Arrange
        const double value = 10.5;

        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.DoubleProperty <= value);

        // Assert
        await Assert.That(result).IsEqualTo($"(DoubleProperty le {value.ToString(CultureInfo.InvariantCulture)})");
    }

    [Test]
    public async Task Build_With_AndAlso_Expression()
    {
        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.StringProperty == "test" && x.IntProperty > 10);

        // Assert
        await Assert.That(result).IsEqualTo("((StringProperty eq 'test') and (IntProperty gt 10))");
    }

    [Test]
    public async Task Build_With_OrElse_Expression()
    {
        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.StringProperty == "test" || x.IntProperty > 10);

        // Assert
        await Assert.That(result).IsEqualTo("((StringProperty eq 'test') or (IntProperty gt 10))");
    }

    [Test]
    public async Task Build_With_NotEqual_Expression()
    {
        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.IntProperty != 10);

        // Assert
        await Assert.That(result).IsEqualTo("(IntProperty ne 10)");
    }

    [Test]
    public async Task Build_With_Complex_Expression()
    {
        var guid = new Guid("e7d72a8b-09a1-4349-8169-63d301f120bf");

        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => (x.StringProperty == "test" && x.IntProperty > 10) || x.GuidProperty == guid);

        // Assert
        await Assert.That(result).IsEqualTo("(((StringProperty eq 'test') and (IntProperty gt 10)) or (GuidProperty eq guid'e7d72a8b-09a1-4349-8169-63d301f120bf'))");
    }

    [Test]
    public async Task Build_With_External_Variable()
    {
        // Arrange
        var myVar = "hello";

        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.StringProperty == myVar);

        // Assert
        await Assert.That(result).IsEqualTo("(StringProperty eq 'hello')");
    }

    [Test]
    public async Task Build_With_External_Method_Call()
    {
        // Act
        var result = FilterBuilder<FilterTestModel>.Build(x => x.StringProperty == GetStringValue());

        // Assert
        await Assert.That(result).IsEqualTo("(StringProperty eq 'from_method')");
    }

    private static string GetStringValue() => "from_method";

    private class FilterTestModel
    {
        public string StringProperty { get; set; } = string.Empty;

        public string? NullableStringProperty { get; set; }

        public bool BoolProperty { get; set; }

        public bool? NullableBoolProperty { get; set; }

        public long LongProperty { get; set; }

        public long? NullableLongProperty { get; set; }

        public Guid GuidProperty { get; set; }

        public Guid? NullableGuidProperty { get; set; }

        public DateTimeOffset DateTimeOffsetProperty { get; set; }

        public DateTimeOffset? NullableDateTimeOffsetProperty { get; set; }

        public DateTime DateTimeProperty { get; set; }

        public DateTime? NullableDateDateTimeProperty { get; set; }

        public int IntProperty { get; set; }

        public int? NullableIntProperty { get; set; }

        public double DoubleProperty { get; set; }

        public double? NullableDoubleProperty { get; set; }

        public short ShortProperty { get; set; }

        public short? NullableShortProperty { get; set; }
    }
}