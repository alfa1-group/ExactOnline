using System.Linq.Expressions;
using ExactOnline.Api.Client.Builders.Select;

namespace ExactOnline.Api.Client.Builders.OrderBy;

public static class OrderByBuilder<T>
{
    public static IOrderedBuilder<T> OrderBy(Expression<Func<T, object?>> expression)
    {
        var propertyName = SelectBuilder<T>.GetPropertyName(expression);
        return new OrderedBuilder<T>($"{propertyName} asc");
    }

    public static IOrderedBuilder<T> OrderByDescending(Expression<Func<T, object?>> expression)
    {
        var propertyName = SelectBuilder<T>.GetPropertyName(expression);
        return new OrderedBuilder<T>($"{propertyName} desc");
    }
}