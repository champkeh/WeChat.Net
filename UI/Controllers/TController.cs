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

            var ret = Wx.Utils.CommonUtil.GetQRCodeTicket( token, "100/2" );
            
            return Content( "" );
        }



    }
}