#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MathCore.ASP.WEB.Tests.OData
{
    public static class HttpClientExtensions
    {
        public static IQueryable<T> OData<T>(this HttpClient client) => new ODataQuery<T>(client);
    }

    internal class ODataQuery<T> : IQueryable<T>
    {
        public ODataQuery(HttpClient Client)
        {
            Provider = new ODataQueryProvider(Client);
            Expression = Expression.Constant(this);
        }

        public ODataQuery(IQueryProvider Provider, Expression Expression)
        {
            this.Provider = Provider;
            this.Expression = Expression;
        }

        public Type ElementType => Expression.Type;
        
        public Expression Expression { get; }
        
        public IQueryProvider Provider { get; }

        public IEnumerator<T> GetEnumerator() => 
            ((IEnumerable<T>)(Provider.Execute(Expression) ?? throw new InvalidOperationException())).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class ODataQueryProvider : IQueryProvider
    {
        private readonly HttpClient _Client;

        public ODataQueryProvider(HttpClient Client) => _Client = Client;
        
        public IQueryable CreateQuery(Expression expression)
        {
            var expression_type = expression.Type;
            var query_type = typeof(ODataQuery<>).MakeGenericType(expression_type);
            return (IQueryable)(Activator.CreateInstance(query_type, this, expression) ?? throw new InvalidOperationException());
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new ODataQuery<TElement>(this, expression);

        public TResult Execute<TResult>(Expression expression) => (TResult)Execute(expression);
        public object Execute(Expression expression)
        {
            var builder = new ODataQueryBuilder();
            builder.Visit(expression);

            var query = builder.Query;

            var type = typeof(IEnumerable<>).MakeGenericType(expression.Type.GenericTypeArguments[0]);

            var result = _Client.GetFromJsonAsync(query, type);

            return result;
        }
    }

    internal class ODataQueryBuilder : ExpressionVisitor
    {
        private readonly StringBuilder _Query = new("?");

        public string Query => _Query.ToString();

        protected override Expression VisitBinary(BinaryExpression node)
        {
            return base.VisitBinary(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            return base.VisitUnary(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            return base.VisitConstant(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _Query.Append(node.Member.Name);
            return base.VisitMember(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            switch (node.Method.DeclaringType?.FullName)
            {
                default: throw new NotSupportedException($"Метод {node.Method} не поддерживается");
                case "System.Linq.Queryable": break;
            }

            switch (node.Method.Name)
            {
                default: throw new NotSupportedException($"Метод {node.Method} не поддерживается");
                case nameof(Queryable.Select):
                    _Query.Append("select=");
                    break;
            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }
    }
}
