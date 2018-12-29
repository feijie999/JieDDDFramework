using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using JieDDDFramework.Core.Exceptions;
using MediatR;

namespace JieDDDFramework.Core.MediatR.Behaviors
{
   public class ValidatorBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse>
   {
       private readonly IValidator<TRequest>[] _validators;

       public ValidatorBehavior(IValidator<TRequest>[] validators) => _validators = validators;

       public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,RequestHandlerDelegate<TResponse> next)
       {
           var failures = _validators
               .Select(v => v.Validate(request))
               .SelectMany(result => result.Errors)
               .Where(x => x != null)
               .ToList();

           if (failures.Any())
           {
               throw new KnownException(
                   $"Command Validation Errors for type {typeof(TRequest).Name}", new ValidationException("Validation exception", failures));
            }

           var response = await next();
           return response;
       }
   }
}
