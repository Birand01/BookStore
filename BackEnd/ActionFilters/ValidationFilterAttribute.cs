using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BackEnd.ActionFilters
{
    public class ValidationFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var action=context.RouteData.Values["action"];
            var controller=context.RouteData.Values["controller"];

            var param=context.ActionArguments.SingleOrDefault(x=>x.Key.Contains("Dto")).Value;
            if(param is null)
            {
                context.Result=new BadRequestObjectResult($"Object is null. Controller:{controller}, action:{action}");
                return;//400
            }
            if(!context.ModelState.IsValid)
            {
                context.Result=new 
                UnprocessableEntityObjectResult(context.ModelState);
            }
            
            
        }
    }
}