namespace ExpressionFramework.Domain;

public static class EnumerableExpression
{
    public static Result<object?> GetResultFromEnumerable(IEnumerable e,
                                                          Func<IEnumerable<object?>, IEnumerable<Result<object?>>> @delegate)
    {
        var results = @delegate(e.OfType<object?>()).TakeWhileWithFirstNonMatching(x => x.IsSuccessful()).ToArray();
        if (!results.Last().IsSuccessful())
        {
            return results.Last();
        }

        return Result<object?>.Success(results.Select(x => x.Value));
    }

    public static Result<IEnumerable<object?>> GetTypedResultFromEnumerable(IEnumerable e,
                                                                            Func<IEnumerable<object?>, IEnumerable<Result<object?>>> @delegate)
    {
        var results = @delegate(e.OfType<object?>()).TakeWhileWithFirstNonMatching(x => x.IsSuccessful()).ToArray();
        if (!results.Last().IsSuccessful())
        {
            return Result<IEnumerable<object?>>.FromExistingResult(results.Last());
        }

        return Result<IEnumerable<object?>>.Success(results.Select(x => x.Value));
    }

    public static Result<T> GetRequiredScalarValue<T>(object? context,
                                                      Expression? predicate,
                                                      Func<IEnumerable<object?>, Result<T>> delegateWithoutPredicate,
                                                      Func<IEnumerable<ItemResult>, Result<T>>? delegateWithPredicate = null,
                                                      Func<IEnumerable<object?>, Result<IEnumerable<object?>>>? selectorDelegate = null,
                                                      bool predicateIsRequired = false)
        => GetScalarValue
        (
            context,
            predicate,
            delegateWithoutPredicate,
            delegateWithPredicate,
            _ => Result<T>.Invalid("Enumerable is empty"),
            _ => Result<T>.Invalid("None of the items conform to the supplied predicate"),
            selectorDelegate,
            predicateIsRequired
        );

    public static Result<T> GetOptionalScalarValue<T>(object? context,
                                                      Expression? predicate,
                                                      Func<IEnumerable<object?>, Result<T>> delegateWithoutPredicate,
                                                      Func<IEnumerable<ItemResult>, Result<T>>? delegateWithPredicate = null,
                                                      Func<object?, Result<T>>? defaultValueDelegate = null,
                                                      Func<IEnumerable<object?>, Result<IEnumerable<object?>>>? selectorDelegate = null,
                                                      bool predicateIsRequired = false)
        => GetScalarValue
        (
            context,
            predicate,
            delegateWithoutPredicate,
            delegateWithPredicate,
            defaultValueDelegate,
            defaultValueDelegate,
            selectorDelegate,
            predicateIsRequired
        );

    public static Result<T> GetAggregateValue<T>(object? context,
                                                 Func<IEnumerable<object?>, Result<T>> aggregateDelegate,
                                                 Expression? selectorExpression = null)
        => context is IEnumerable e
            ? GetTypedResultFromEnumerable(e, x => x
                .Select(y => selectorExpression == null
                    ? Result<object?>.Success(y)
                    : selectorExpression.Evaluate(y)))
                .Transform(result => result.IsSuccessful()
                    ? aggregateDelegate.Invoke(result.Value!)
                    : Result<T>.FromExistingResult(result))
            : GetInvalidResult<T>(context);

    public static Result<object?> GetInvalidResult(object? context)
        => GetInvalidResult<object?>(context);

    public static Result<T> GetInvalidResult<T>(object? context)
        => context.Transform(x => Result<T>.Invalid(x == null
                ? "Context cannot be empty"
                : "Context is not of type enumerable"));
    
    public static Result<object?> GetDefaultValue(Expression? defaultExpression, object? context)
        => defaultExpression == null
            ? new EmptyExpression().Evaluate(context)
            : defaultExpression.Evaluate(context);

    public static IEnumerable<ValidationResult> ValidateContext(object? context,
                                                                Func<IEnumerable<ValidationResult>>? additionalValidationErrorsDelegate = null)
    {
        if (context == null)
        {
            yield return new ValidationResult("Context cannot be empty");
        }
        else if (context is not IEnumerable)
        {
            yield return new ValidationResult("Context is not of type enumerable");
        }

        if (additionalValidationErrorsDelegate != null)
        {
            foreach (var error in additionalValidationErrorsDelegate.Invoke())
            {
                yield return error;
            }
        }
    }

    public static IEnumerable<ValidationResult> ValidateEmptyEnumerable(object? context)
    {
        if (context is not IEnumerable e)
        {
            yield break;
        }

        if (!e.OfType<object?>().Any())
        {
            yield return new ValidationResult("Enumerable is empty");
        }
    }

    public static IEnumerable<ValidationResult> ValidateEmptyPredicate(Expression? predicateExpression)
    {
        if (predicateExpression == null)
        {
            yield return new ValidationResult("Predicate is required");
        }
    }

#pragma warning disable S107 // Methods should not have too many parameters
    public static ExpressionDescriptor GetDescriptor(Type type,
                                                     string description,
                                                     string okValue,
                                                     string okDescription,
                                                     string invalidDescription,
                                                     string errorDescription,
                                                     bool hasDefaultExpression,
                                                     bool predicateIsRequired = false)
#pragma warning restore S107 // Methods should not have too many parameters
        => new(
            type.Name,
            type.FullName,
            description,
            true,
            typeof(IEnumerable).FullName,
            "Enumerable value to use",
            true,
            new[]
            {
                new ParameterDescriptor("PredicateExpression", typeof(Expression).FullName, predicateIsRequired ? "Predicate to use" : "Optional predicate to use", predicateIsRequired),
                new ParameterDescriptor("DefaultExpression", typeof(Expression).FullName, "Optional default value to use", false),
            }.Where(x => x.Name != "DefaultExpression" || hasDefaultExpression),
            new[]
            {
                new ReturnValueDescriptor(ResultStatus.Ok, okValue, typeof(object), okDescription),
                new ReturnValueDescriptor(ResultStatus.Invalid, "Empty", typeof(object), invalidDescription),
                new ReturnValueDescriptor(ResultStatus.Error, "Empty", typeof(object), errorDescription),
            });

#pragma warning disable S107 // Methods should not have too many parameters
    private static Result<T> GetScalarValue<T>(object? context,
                                               Expression? predicate,
                                               Func<IEnumerable<object?>, Result<T>> delegateWithoutPredicate,
                                               Func<IEnumerable<ItemResult>, Result<T>>? delegateWithPredicate,
                                               Func<object?, Result<T>>? defaultValueDelegateWithoutPredicate,
                                               Func<object?, Result<T>>? defaultValueDelegateWithPredicate,
                                               Func<IEnumerable<object?>, Result<IEnumerable<object?>>>? selectorDelegate = null,
                                               bool predicateIsRequired = false)
#pragma warning restore S107 // Methods should not have too many parameters
    {
        if (context == null)
        {
            return Result<T>.Invalid("Context cannot be empty");
        }
        
        if (context is not IEnumerable e)
        {
            return Result<T>.Invalid("Context is not of type enumerable");
        }

        if (predicateIsRequired && predicate == null)
        {
            return Result<T>.Invalid("Predicate is required");
        }

        if (selectorDelegate == null)
        {
            selectorDelegate = new Func<IEnumerable<object?>, Result<IEnumerable<object?>>>(x => Result<IEnumerable<object?>>.Success(x));
        }

        var itemsResult = selectorDelegate.Invoke(e.OfType<object?>());
        if (!itemsResult.IsSuccessful())
        {
            return Result<T>.FromExistingResult(itemsResult);
        }

        if (predicate == null)
        {
            if (!itemsResult.Value.Any() && defaultValueDelegateWithoutPredicate != null)
            {
                return defaultValueDelegateWithoutPredicate.Invoke(context);
            }

            return delegateWithoutPredicate.Invoke(itemsResult.Value!);
        }

        var results = itemsResult.Value.Select(x => new ItemResult
        (
            x,
            predicate.EvaluateTyped<bool>(x, "Predicate did not return a boolean value")
        )).TakeWhileWithFirstNonMatching(x => x.Result.IsSuccessful());

        if (results.Any(x => !x.Result.IsSuccessful()))
        {
            // Error in predicate evaluation
            return Result<T>.FromExistingResult(results.First(x => !x.Result.IsSuccessful()).Result);
        }

        if (!results.Any(x => x.Result.Value) && defaultValueDelegateWithPredicate != null)
        {
            return defaultValueDelegateWithPredicate.Invoke(context);
        }

        return delegateWithPredicate?.Invoke(results)
            ?? Result<T>.Invalid("DelegateWithPredicate is required when predicate is filled");
    }
}
