using System.Linq.Expressions;

namespace ExactOnline.Api.Client.Builders.Filter;

public class TimestampFilterBuilder
{
    public static string Build(Expression<Func<TimestampFilter, bool>> timestampExpression)
    {
        return FilterBuilder<TimestampFilter>.Build(timestampExpression);
    }
}