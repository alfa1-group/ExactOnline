using System.Linq.Expressions;
using System.Text;

namespace ExactOnline.Api.Client.Builders.Filter;

public class FilterBuilder<T> : ExpressionVisitor, IFilterBuilder
{
	private readonly StringBuilder _filter = new();

    public string Build() => _filter.ToString();

	public static string Build(Expression<Func<T, bool>> expression)
	{
		var builder = new FilterBuilder<T>();
		builder.Visit(expression);
		return builder.Build();
	}

	protected override Expression VisitMethodCall(MethodCallExpression node)
	{
		if (node.Method.Name == "Equals")
		{
			Visit(node.Object);
			_filter.Append(" eq ");
			Visit(node.Arguments[0]);
			return node;
		}

		throw new NotSupportedException($"Method '{node.Method.Name}' not supported");
	}

	protected override Expression VisitBinary(BinaryExpression node)
	{
		_filter.Append('(');
		Visit(node.Left);

		_filter.Append(node.NodeType switch
		{
			ExpressionType.AndAlso => " and ",
			ExpressionType.OrElse => " or ",
			ExpressionType.Equal => " eq ",
			ExpressionType.NotEqual => " ne ",
			ExpressionType.LessThan => " lt ",
			ExpressionType.LessThanOrEqual => " le ",
			ExpressionType.GreaterThan => " gt ",
			ExpressionType.GreaterThanOrEqual => " ge ",
			_ => throw new NotSupportedException($"Operator '{node.NodeType}' not supported"),
		});

		Visit(node.Right);
		_filter.Append(')');

		return node;
	}

	protected override Expression VisitMember(MemberExpression node)
	{
		if (node.Expression?.NodeType == ExpressionType.Parameter)
		{
			_filter.Append(node.Member.Name);
		}
		else
		{
			var value = GetMemberValue(node);
			Visit(Expression.Constant(value));
		}

		return node;
	}

	protected override Expression VisitConstant(ConstantExpression node)
	{
		switch (node.Value)
		{
			case string stringValue:
				_filter.Append($"'{stringValue}'");
				break;

			case bool boolValue:
				_filter.Append(boolValue.ToString().ToLower());
				break;

			case Guid guidValue:
				_filter.Append($"guid'{guidValue}'");
				break;

			case DateTime dateTimeValue:
				_filter.Append($"datetime'{dateTimeValue:o}'");
				break;

			default:
				_filter.Append(node.Value);
				break;
		}

		return node;
	}

	private static object GetMemberValue(MemberExpression member)
	{
		var objectMember = Expression.Convert(member, typeof(object));
		var getterLambda = Expression.Lambda<Func<object>>(objectMember);
		var getter = getterLambda.Compile();
		
        return getter();
	}
}