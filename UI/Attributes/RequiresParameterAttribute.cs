using System;
using System.Reflection;
using System.Web.Mvc;

namespace UI.Attributes
{
    /// <summary>
    /// 参数必须出现
    /// </summary>
    [AttributeUsage( AttributeTargets.Method, AllowMultiple = true, Inherited = false )]
    public class RequiresParameterAttribute : ActionMethodSelectorAttribute
    {
        readonly string parameterName;

        public RequiresParameterAttribute( string parameterName )
        {
            this.parameterName = parameterName;
        }

        public override bool IsValidForRequest( ControllerContext controllerContext, MethodInfo methodInfo )
        {
            return controllerContext.RequestContext.HttpContext.Request.Params[parameterName] != null;
        }
    }
}