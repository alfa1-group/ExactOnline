using System.Linq.Expressions;

namespace ExactOnline.Api.Client.Builders.Filter;

public class SyncFilterBuilder
{
    public static string Build(Expression<Func<SyncFilter, bool>> timestampExpression)
    {
        return FilterBuilder<SyncFilter>.Build(timestampExpression);
    }
}