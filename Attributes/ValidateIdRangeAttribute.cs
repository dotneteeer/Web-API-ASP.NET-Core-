using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI_ASPNET_Core.Attributes;

public class ValidateIdRangeAttribute: ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int startId = (int)context.ActionArguments["startId"];
        int endId = (int)context.ActionArguments["endId"];

        if (endId <= startId)
        {
            context.Result = new BadRequestObjectResult("endId must be greater than startId");
        }
    }
}