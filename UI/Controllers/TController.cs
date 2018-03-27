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

            var user_list = Wx.User.UserManager.SubscribeList( token, null );

            var user_info = Wx.User.UserManager.BatchGetUserInfo( token, user_list.data.openid );


            return Content( "" );
        }


       
    }
}