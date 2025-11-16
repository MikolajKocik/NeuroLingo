using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

namespace NeuroLingo.Utils.ValidationAttribute;

public class ValidateDtoAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)  
    {
        foreach (var arg in context.ActionArguments)
        {
            if (arg.Value == null)
            {
                continue;
            }

            var valResults = new List<ValidationResult>();
            var valContext = new ValidationContext(arg.Value);

            Validator.TryValidateObject(arg.Value, valContext, valResults, validateAllProperties: true);

            foreach (var valResult in valResults)
            {
                if (!valResult.MemberNames.Any())
                {
                    context.ModelState.AddModelError(string.Empty, valResult.ErrorMessage!);
                }
                else
                {
                    foreach (var member in valResult.MemberNames)
                    {
                        context.ModelState.AddModelError(member, valResult.ErrorMessage!);
                    }
                }
            }
        }


        if (!context.ModelState.IsValid)
        {
            var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var viewName = descriptor.ActionName;
            var model = context.ActionArguments.Values.FirstOrDefault();

            var metadataProvider = context.HttpContext.RequestServices
                                           .GetRequiredService<IModelMetadataProvider>();

            context.Result = new ViewResult
            {
                ViewName = viewName,
                ViewData = new ViewDataDictionary(metadataProvider, context.ModelState)
                                  
                {
                    Model = model
                }
            };
        }
    }
}
