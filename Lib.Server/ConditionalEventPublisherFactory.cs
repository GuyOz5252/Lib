using System.Linq.Expressions;
using Lib.Events.Abstract;

namespace Lib.Server;

public class ConditionalEventPublisherFactory<T>
{
    public Type ParameterType { get; set; }
    
    public string PropertyName { get; set; }
    
    public T EqualsTo { get; set; }
    
    public List<Component> PublishIfTrue { get; set; }
    
    public List<Component> PublishIfFalse { get; set; }

    public ConditionalEventPublisher Create()
    {
        var eventParameterExpression = Expression.Parameter(typeof(IEvent));
        
        var castedEventParameterExpression = Expression.Convert(eventParameterExpression, ParameterType);
        
        var publishIfTrueBlock = Expression.Block(PublishIfTrue.Select(component =>
            Expression.Call(Expression.Constant(component), typeof(Component).GetMethod(nameof(Component.InjectEvent))!,
                eventParameterExpression)));
        var publishIfTrue = Expression.Lambda<Action<IEvent>>(publishIfTrueBlock, eventParameterExpression);
        var publishIfFalseBlock = Expression.Block(PublishIfFalse.Select(component =>
            Expression.Call(Expression.Constant(component), typeof(Component).GetMethod(nameof(Component.InjectEvent))!,
                eventParameterExpression)));
        var publishIfFalse = Expression.Lambda<Action<IEvent>>(publishIfFalseBlock, eventParameterExpression);
        
        var propertyExpression = Expression.Property(castedEventParameterExpression, PropertyName);
        
        var conditionExpression = Expression.Equal(propertyExpression, Expression.Constant(EqualsTo));
        
        var publishIfTrueExpression = Expression.Invoke(publishIfTrue, eventParameterExpression);
        var publishIfFalseExpression = Expression.Invoke(publishIfFalse, eventParameterExpression);
        
        var expression = Expression.Condition(conditionExpression, publishIfTrueExpression, publishIfFalseExpression);
        var lambdaExpression = Expression.Lambda<Action<IEvent>>(expression, eventParameterExpression).Compile();

        return new ConditionalEventPublisher(lambdaExpression);
    }
}