using System.Linq.Expressions;
using System.Text;

namespace ExactOnline.Api.Client.Builders;

public static class OrderByBuilder<T>
{
    public static IOrderedQueryBuilder<T> OrderBy(Expression<Func<T, object?>> expression)
    {
        var propertyName = GetPropertyName(expression);
        return new OrderedQueryBuilder<T>($"{propertyName} asc");
    }

    public static IOrderedQueryBuilder<T> OrderByDescending<T>(Expression<Func<T, object?>> expression)
    {
        var propertyName = GetPropertyName(expression);
        return new OrderedQueryBuilder<T>($"{propertyName} desc");
    }

    private static string GetPropertyName<T>(Expression<Func<T, object?>> expression)
    {
        return expression.Body switch
        {
            MemberExpression memberExpression => memberExpression.Member.Name,
            UnaryExpression { Operand: MemberExpression memberExpr } => memberExpr.Member.Name,
            _ => throw new ArgumentException($"Expression must be a property access. Found {expression.Body.GetType().Name} instead. Example: x => x.PropertyName", nameof(expression))
        };
    }
}

public interface IOrderedQueryBuilder<T>
{
    IOrderedQueryBuilder<T> ThenBy(Expression<Func<T, object?>> expression);
    IOrderedQueryBuilder<T> ThenByDescending(Expression<Func<T, object?>> expression);
    string Build();
}

internal class OrderedQueryBuilder<T> : IOrderedQueryBuilder<T>
{
    private readonly StringBuilder _builder;

    internal OrderedQueryBuilder(string initialOrderBy)
    {
        _builder = new StringBuilder(initialOrderBy);
    }

    public IOrderedQueryBuilder<T> ThenBy(Expression<Func<T, object?>> expression)
    {
        AddOrderBy(expression, "asc");
        return this;
    }

    public IOrderedQueryBuilder<T> ThenByDescending(Expression<Func<T, object?>> expression)
    {
        AddOrderBy(expression, "desc");
        return this;
    }

    private void AddOrderBy(Expression<Func<T, object?>> expression, string direction)
    {
        _builder.Append(", ");
        var propertyName = GetPropertyName(expression);
        _builder.Append($"{propertyName} {direction}");
    }

    public string Build() => _builder.ToString();

    private static string GetPropertyName(Expression<Func<T, object?>> expression)
    {
        return expression.Body switch
        {
            MemberExpression memberExpression => memberExpression.Member.Name,
            UnaryExpression { Operand: MemberExpression memberExpr } => memberExpr.Member.Name,
            _ => throw new ArgumentException($"Expression must be a property access. Found {expression.Body.GetType().Name} instead. Example: x => x.PropertyName", nameof(expression))
        };
    }
}