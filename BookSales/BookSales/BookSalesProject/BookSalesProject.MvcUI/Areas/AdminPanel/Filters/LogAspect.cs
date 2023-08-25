using Microsoft.AspNetCore.Mvc.Filters;

namespace BookSalesProject.MvcUI.Areas.AdminPanel.Filters
{
    public class LogAspect : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // metod çalışmaya başladığında
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            // geriye değer dönmeden hemen önce
        }
    }
}
