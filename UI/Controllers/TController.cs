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
            //var token = Wx.Utils.AccessTokenUtil.GetAccessToken( );


            //var black_list = Wx.User.UserManager.GetBlackList( token, null );


            //var ret = Wx.User.UserManager.BatchUnblockList( token, black_list.data.openid );

            return Content( "" );
        }



    }
}