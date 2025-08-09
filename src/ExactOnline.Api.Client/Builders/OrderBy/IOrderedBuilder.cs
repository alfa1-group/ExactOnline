using System.Linq.Expressions;

namespace ExactOnline.Api.Client.Builders.OrderBy;

public interface IOrderedBuilder<T>
{
    IOrderedBuilder<T> ThenBy(Expression<Func<T, object?>> expression);

    IOrderedBuilder<T> ThenByDescending(Expression<Func<T, object?>> expression);

    string Build();
}