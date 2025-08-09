using System.Linq.Expressions;

namespace ExactOnline.Api.Client.Builders.Select;

public static class SelectBuilder<T>
{
    /// <summary>
    /// Creates a CSV string of property names from the provided lambda expressions.
    /// </summary>
    /// <typeparam name="T">The type to extract property names from</typeparam>
    /// <param name="expressions">Lambda expressions pointing to properties</param>
    /// <returns>A comma-separated string of property names</returns>
    public static string Build(params Expression<Func<T, object?>>[] expressions)
    {
        if (expressions.Length == 0)
        {
            return string.Empty;
        }

        var propertyNames = new List<string>();

        foreach (var expression in expressions)
        {
            var propertyName = GetPropertyName(expression);
            if (!string.IsNullOrEmpty(propertyName))
            {
                propertyNames.Add(propertyName);
            }
        }

        return string.Join(", ", propertyNames);
    }

    /// <summary>
    /// Creates a CSV string of property names from an anonymous object expression.
    /// </summary>
    /// <typeparam name="T">The source type</typeparam>
    /// <param name="expression">Lambda expression that returns an anonymous object with the desired properties</param>
    /// <returns>A comma-separated string of property names</returns>
    public static string Build(Expression<Func<T, object>> expression)
    {
        return expression.Body switch
        {
            NewExpression newExpression => ExtractFromNewExpression(newExpression),
            MemberExpression memberExpression => memberExpression.Member.Name,
            UnaryExpression { Operand: MemberExpression memberExpr } => memberExpr.Member.Name,
            _ => throw new ArgumentException($"Expression must be a property access or anonymous object constructor. Found {expression.Body.GetType().Name} instead. Examples: x => x.PropertyName or x => new {{ x.Prop1, x.Prop2 }}", nameof(expression))
        };
    }

    private static string ExtractFromNewExpression(NewExpression newExpression)
    {
        var propertyNames = new List<string>();

        foreach (var argExpr in newExpression.Arguments)
        {
            var propertyName = argExpr switch
            {
                MemberExpression memberExpr => memberExpr.Member.Name,
                UnaryExpression { Operand: MemberExpression unaryMemberExpr } => unaryMemberExpr.Member.Name,
                _ => throw new ArgumentException($"Invalid expression in anonymous object: {argExpr.GetType().Name}")
            };

            propertyNames.Add(propertyName);
        }

        return string.Join(", ", propertyNames);
    }

    internal static string GetPropertyName(Expression<Func<T, object?>> expression)
    {
        return expression.Body switch
        {
            MemberExpression memberExpression => memberExpression.Member.Name,
            UnaryExpression { Operand: MemberExpression memberExpr } => memberExpr.Member.Name,
            _ => throw new ArgumentException($"Expression must be a property access. Found {expression.Body.GetType().Name} instead. Example: x => x.PropertyName", nameof(expression))
        };
    }
}