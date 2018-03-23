using System.Web.Mvc;

namespace UI.Controllers
{
    public class TController : Controller
    {
        /// <summary>
        /// 用于测试一些小功能
        /// </summary>
        /// <returns></returns>
        public ActionResult Get()
        {
            var token = Wx.Utils.AccessTokenUtil.GetAccessToken( );

            var iplist = Wx.Menu.MenuManager.Create( token );

            return Content( "" );
        }
    }
}