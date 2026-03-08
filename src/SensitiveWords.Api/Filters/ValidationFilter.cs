using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ValidationFilter> _logger;

    public ValidationFilter(
        IServiceProvider serviceProvider,
        ILogger<ValidationFilter> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var failures = new List<ValidationFailure>();

        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null)
                continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            var validator = _serviceProvider.GetService(validatorType) as IValidator;

            if (validator is null)
                continue;

            var validationContext = new ValidationContext<object>(argument);

            ValidationResult result;

            try
            {
                result = await validator.ValidateAsync(validationContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Validation failed due to exception.");
                throw;
            }

            if (!result.IsValid)
            {
                failures.AddRange(result.Errors);
            }
        }

        if (failures.Count > 0)
        {
            _logger.LogWarning(
                "Validation failed for {Path}. Errors: {Count}",
                context.HttpContext.Request.Path,
                failures.Count);

            var errors = failures
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());

            context.Result = new BadRequestObjectResult(
                new ValidationProblemDetails(errors));

            return;
        }

        await next();
    }
}