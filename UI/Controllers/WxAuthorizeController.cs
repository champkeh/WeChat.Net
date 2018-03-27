using System;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    /// <summary>
    /// 微信网页授权
    /// </summary>
    public class WxAuthorizeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult Auth( string returnUrl )
        {
            if ( string.IsNullOrWhiteSpace( returnUrl ) )
            {
                throw new Exception( "授权回调缺少返回页面的参数" );
            }
            else
            {
                // 把<回调地址>编码进微信网页授权接口的return_url参数中
                returnUrl = "?returnUrl=" + HttpUtility.UrlEncode( returnUrl );
            }

            string return_url = Wx.Config.Domain + "/WxAuthorize/AuthCallback" + returnUrl;
            var url = Wx.Authorize.WebAuthorizeUtil.GetAuthUri( Wx.Config.AppID, "snsapi_userinfo", "", return_url );

            return Redirect( url );
        }


        public ActionResult AuthCallback( string code, string state, string returnUrl )
        {
            if ( !string.IsNullOrEmpty( code ) )
            {
                // 用code获取Access_token
                var access_token = Wx.Authorize.WebAuthorizeUtil.GetAccessToken( Wx.Config.AppID, Wx.Config.AppSecret, code );
                if ( access_token.errcode != 0 )
                {
                    Log.Logger.Log( "[WxAuthorize: 根据code获取access_token失败] " + access_token.errmsg );
                    return RedirectToAction( "Index", "Home" );
                }

                // 获取用户信息
                var user_info = Wx.Authorize.WebAuthorizeUtil.GetUserInfo( access_token.access_token, access_token.openid );
                if ( user_info.errcode != 0 )
                {
                    Log.Logger.Log( "[WxAuthorize: 根据access_token获取用户信息失败] " + user_info.errmsg );
                    return RedirectToAction( "Index", "Home" );
                }


                // 此处可以保存用户信息到数据库
                //user.platform = state;
                //user.subscribe = WxHelper.AuthHelper.UserIsSubscribe( new DALWxAcToken( ).GetAccessToken( "Ac" ), auth.openid );
                //BLLCustomer bll = new BLLCustomer( );
                //bll.InsertOrUpdateWxUser( user );


                // 用户信息保存到session服务器上
                var guid_sid = Guid.NewGuid( ).ToString( "N" );
                HttpCookie cookie_sid = new HttpCookie( "wx_authorize", guid_sid );
                cookie_sid.HttpOnly = true;
                cookie_sid.Expires = DateTime.Now.AddHours( 2 );
                Response.Cookies.Add( cookie_sid );

                // 设置redis缓存
                Cache.MemoryCacheProvider.Append( guid_sid, new Cache.Models.WebAuthorizeUserModel
                {
                    nickname = user_info.nickname,
                    headimgurl = user_info.headimgurl,
                    openid = user_info.openid,
                    province = user_info.province,
                } );

                if ( !string.IsNullOrWhiteSpace( returnUrl ) )
                {
                    return Redirect( HttpUtility.UrlDecode( returnUrl ) );
                }
                else
                {
                    return RedirectToAction( "Index", "Home" );
                }
            }
            else
            {
                Log.Logger.Log( "[WxAuthorize: 回调中没有携带code参数]" );
                return RedirectToAction( "Index", "Home" );
            }
        }

    }
}