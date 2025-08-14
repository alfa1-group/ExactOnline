using System.Linq.Expressions;

namespace ExactOnline.Api.Client.Builders.Filter;

public class TimestampFilterBuilder
{
    private static readonly ExpressionType[] AllowedExpressionTypes = [ExpressionType.Equal, ExpressionType.GreaterThan, ExpressionType.GreaterThanOrEqual];

    public static string Build(Expression<Func<SyncFilter, bool>> timestampExpression)
    {
        if (!AllowedExpressionTypes.Contains(timestampExpression.Body.NodeType))
        {
            throw new ArgumentException("Timestamp expression only supports `==`, `>` or `>=`.", nameof(timestampExpression));
        }

        return FilterBuilder<SyncFilter>.Build(timestampExpression);
    }
}