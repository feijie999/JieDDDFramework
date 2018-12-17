using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using JieDDDFramework.Web.Models;
using JieDDDFramework.Web.ModelValidate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace JieDDDFramework.Web.Filters
{
    public class ValidateModelStateFilter :  IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }
            if (context.HttpContext.Items.TryGetValue(ValidatorAttribute.VALIDATOR_ITEM, out var obj))
            {
                if (obj is IList<ValidationFailure> validationFailures&& validationFailures.Any())
                {
                    context.Result = new BadRequestObjectResult(new ModelErrorResult(validationFailures.First()));
                    return;
                }
            }
      
            var result = new ModelErrorResult(context.ModelState);
            context.Result = new BadRequestObjectResult(result);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
