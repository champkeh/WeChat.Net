using System.Web.Mvc;

namespace UI.Attributes
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting( ActionExecutingContext filterContext )
        {
            var req = filterContext.RequestContext.HttpContext.Request;
            var logContent = req.Url.ToString( ) + "|" + req.UserHostAddress + "|" + req.UserHostName;

            Log.Logger.Log( "[log: 请求日志] " + logContent );

            base.OnActionExecuting( filterContext );
        }
    }
}