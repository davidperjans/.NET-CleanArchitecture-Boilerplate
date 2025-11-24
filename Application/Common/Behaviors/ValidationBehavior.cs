using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                var errorMessages = failures.Select(e => e.ErrorMessage).ToList();

                var responseType = typeof(TResponse);
                if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(OperationResult<>))
                {
                    var resultType = responseType.GetGenericArguments()[0];
                    var failureMethod = typeof(OperationResult<>).MakeGenericType(resultType)
                        .GetMethod("Failure", new[] { typeof(IEnumerable<string>) });

                    return (TResponse)failureMethod!.Invoke(null, new object[] { errorMessages })!;
                }

                throw new FluentValidation.ValidationException(failures);
            }

            return await next();
        }
    }
}
