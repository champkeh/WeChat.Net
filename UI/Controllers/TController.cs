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


        public ActionResult Index()
        {
            // 根据cookie获取用户信息，如果cookie丢失，则重新进行网页授权
            var cookie = Request.Cookies.Get( "wx_authorize" );
            if ( cookie == null || !Cache.MemoryCacheProvider.Exist( cookie.Value ) )
            {
                Log.Logger.Log( "[ui: 缓存中不存在用户信息]" );
                return RedirectToAction( "Auth", "WxAuthorize", new { returnurl = Url.Action( "Index", "T" ) } );
            }


            // 从redis中获取用户信息
            var user = Cache.MemoryCacheProvider.Get( cookie.Value );
            Log.Logger.Log( "[ui: 当前用户是: " + user.nickname + "]" );

            ViewBag.Nick = user.nickname;
            return View( );
        }


    }
}