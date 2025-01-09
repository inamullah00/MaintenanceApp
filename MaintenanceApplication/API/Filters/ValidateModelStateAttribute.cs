using Maintenance.Application.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Maintenance.API.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {

            if (!context.ModelState.IsValid) 
            {

                var errorsData = new Dictionary<string, string>();

                foreach (var modelStateErrors in context.ModelState)
                {
                    var Key = modelStateErrors.Key;

                    if (Key.Contains("."))
                    {
                        Key = Key.Split(".").Last();
                    }

                    var errors = modelStateErrors.Value.Errors;
                    if (errors != null && errors.Count > 0)
                    {
                        foreach (var item in errors)
                        {

                           if(!errorsData.ContainsKey(Key))
                            {
                                errorsData.Add(Key,item.ErrorMessage);
                            }

                        }
                    }
                }
                // Return a failure Result with the error details and set the response
                var response = Result<object>.Failure(errorsData.ToString(), "Model validation failed", StatusCodes.Status400BadRequest);
                context.Result = new JsonResult(response) { StatusCode = StatusCodes.Status400BadRequest };
            }
        }
    }
}
