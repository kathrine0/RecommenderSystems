using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;
using System.Reflection;

namespace Recommender.Common
{
    //public static class LinqExtensions
    //{
    //    public static void AddIfNotExists<T>(this IList<T> list, Expression<Func<T, object>> predicate, IList<T> elements) where T : class, new()
    //    {
    //        features.Where(d => !allFeatures
    //                .Any(c => c.Name == d.Name && c.FeatureCategory == d.FeatureCategory))
    //                .ToList()
    //                .ForEach(f => allFeatures.Add(f));

    //        throw new NotImplementedException();
    //    }
    //}

    //public class AddIfNotExistsFluentInterface<T> where T : class, new()
    //{
    //    private IEnumerable<T> _newElements;
    //    private IList<T> _list;
    //    private IDictionary<T, IList<Expression>> _partialExpressions = new Dictionary<T, IList<Expression>>();
    //    private ParameterExpression _pe;

    //    public AddIfNotExistsFluentInterface(IList<T> list, IEnumerable<T> elements)
    //    {
    //        _newElements = elements;
    //        _list = list;
    //        _pe = Expression.Parameter(typeof(T), "x");
    //    }

    //    public AddIfNotExistsFluentInterface<T> Where(Expression<Func<T, object>> predicate)
    //    {
    //        foreach (T entity in _newElements)
    //        {
    //            _partialExpressions.Add(entity, new List<Expression>());
    //            AddPartialExpression(predicate, entity);
    //        }
    //        return this;
    //    }

    //    public void Execute()
    //    {
    //        foreach (T entity in _newElements)
    //        {
    //            AddIfNotExists(entity);
    //        }
    //    }

    //    private void AddPartialExpression(Expression<Func<T, object>> predicate, T entity)
    //    {
    //        var expressions = new List<Expression>();
    //        List<string> propertyNames = GetPropertyNames(predicate);

    //        if (propertyNames.Count() == 0)
    //        {
    //            throw new ArgumentNullException();
    //        }

    //        propertyNames.ForEach(propertyName =>
    //        {
    //            PropertyInfo propertyInfo = typeof(T).GetProperty(propertyName);
    //            Expression left = Expression.Property(_pe, propertyInfo);
    //            Expression right = Expression.Constant(propertyInfo.GetValue(entity, null), propertyInfo.PropertyType);

    //            expressions.Add(Expression.Equal(left, right));
    //        });

    //        Expression partialExpression = expressions[0];

    //        expressions.Skip(1).ToList().ForEach(expression =>
    //        {
    //            partialExpression = Expression.And(partialExpression, expression);
    //        });

    //        _partialExpressions[entity].Add(partialExpression);
    //    }

    //    private void AddIfNotExists(T entity)
    //    {
    //        IList<Expression> expressions = _partialExpressions[entity];

    //        Expression finalExpression = expressions[0];

    //        expressions.Skip(1).ToList().ForEach(expression =>
    //        {
    //            finalExpression = Expression.Or(finalExpression, expression);
    //        });

    //        Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(finalExpression, new ParameterExpression[] { _pe });

    //        //if (!_list.Any(lambda))
    //        //{
    //        //    _list.Add(entity);
    //        //}
    //    }

    //    //inspired by https://gist.github.com/sandord/400553/6562ebb3cf2767d6c1ad9474d6f04691ab6ca412
    //    private List<string> GetPropertyNames(Expression<Func<T, object>> property)
    //    {
    //        LambdaExpression lambda = (LambdaExpression)property;
    //        List<string> propertyNames = new List<string>();

    //        if (lambda.Body is UnaryExpression)
    //        {
    //            UnaryExpression unaryExpression = (UnaryExpression)(lambda.Body);
    //            MemberExpression memberExpression = (MemberExpression)(unaryExpression.Operand);

    //            propertyNames.Add(memberExpression.Member.Name);
    //        }
    //        else if (lambda.Body is NewExpression)
    //        {
    //            NewExpression newExpression = (NewExpression)(lambda.Body);
    //            var arguments = newExpression.Arguments;

    //            foreach (Expression argument in arguments)
    //            {
    //                propertyNames.Add(ParseExpression(argument));
    //            }
    //        }
    //        else
    //        {
    //            propertyNames.Add(ParseExpression(lambda.Body));
    //        }

    //        return propertyNames;
    //    }

    //    private string ParseExpression(Expression property)
    //    {
    //        MemberExpression memberExpression = property as MemberExpression;

    //        if (memberExpression != null)
    //        {
    //            return memberExpression.Member.Name;
    //        }
    //        else
    //        {
    //            throw new InvalidOperationException("Nested properties are not supported");
    //        }
    //    }

    //}
}
