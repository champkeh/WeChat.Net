using System.Web.Mvc;

namespace UI.Attributes
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting( ActionExecutingContext filterContext )
        {
            var req = filterContext.RequestContext.HttpContext.Request;
            var logContent = req.Url.ToString( );

            Log.Logger.Log( "[log: 请求日志 来自: " + req.UserHostAddress + "] " + logContent );

            base.OnActionExecuting( filterContext );
        }
    }
}