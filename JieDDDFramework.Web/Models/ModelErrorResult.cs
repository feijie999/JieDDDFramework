using System.Collections.Generic;
using System.Linq;
using JieDDDFramework.Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JieDDDFramework.Web.Models
{
    public class ModelErrorResult : ApiResult
    {
        public ModelStateDictionary ModelStateDictionary { get; set; }

        public ModelErrorResult(ModelStateDictionary modelStateDictionary)
        {
            Success = false;
            Code = -1;
            ModelStateDictionary = modelStateDictionary;
            Message = GetValidationErrors().FirstOrDefault();
        }

        private List<string> GetValidationErrors()
        {
            return ModelStateDictionary
                .Keys
                .SelectMany(k => ModelStateDictionary[k].Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
        }
    }
}
