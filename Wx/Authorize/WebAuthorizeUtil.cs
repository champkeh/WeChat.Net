using AppUtils;
using System;
using System.Web;
using Wx.Authorize.Models;

namespace Wx.Authorize
{
    public class WebAuthorizeUtil
    {
        /// <summary>
        /// 获取公众号appid的微信网页授权地址
        /// </summary>
        /// <param name="appid">公众号的appid</param>
        /// <param name="scope">授权模式: (snsapi_base|snsapi_userinfo)</param>
        /// <param name="state">自定义参数，重定向后会带上该参数，可以填写a-zA-Z0-9的参数值，最多128字节</param>
        /// <param name="redirect_uri">授权之后的回调地址</param>
        /// <returns></returns>
        public static string GetAuthUri( string appid, string scope, string state, string redirect_uri )
        {
            // 对回调地址进行 url编码
            redirect_uri = HttpUtility.UrlEncode( redirect_uri );

            string url = "https://open.weixin.qq.com/connect/oauth2/authorize";
            url += "?appid=" + appid;
            url += "&redirect_uri=" + redirect_uri;
            url += "&response_type=code";
            url += "&scope=" + scope;
            url += "&state=" + state;
            url += "#wechat_redirect";

            return url;
        }


        /// <summary>
        /// 根据code获取Access_Token，返回内容中还包含用户的OpenID
        /// </summary>
        /// <param name="code">用户授权时获取的code</param>
        /// <returns></returns>
        public static AccessTokenResultModel GetAccessToken( string appid, string appsecret, string code )
        {
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token";
            url += "?appid=" + appid;
            url += "&secret=" + appsecret;
            url += "&code=" + code;
            url += "&grant_type=authorization_code";


            var res = new HttpUtil( ).Get( url );

            Log.Logger.Log( "[wx: GetAccessToken] " + url + "#" + res );

            try
            {
                AccessTokenResultModel wx_token = JsonUtil.FromJson<AccessTokenResultModel>( res );
                if ( wx_token != null && wx_token.errcode == 0 )
                {
                    return wx_token;
                }
                else
                {
                    Log.Logger.Log( "[wx: GetAccessToken Failed] " + url + "#" + res );
                    return null;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: GetAccessToken Exception] " + url + "#" + ex.Message );
                return null;
            }
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static UserInfoResultModel GetUserInfo( string access_token, string openid )
        {
            string url = "https://api.weixin.qq.com/sns/userinfo";
            url += "?access_token=" + access_token;
            url += "&openid=" + openid;
            url += "&lang=zh_CN";

            string res = new HttpUtil( ).Get( url );

            Log.Logger.Log( "[wx: GetUserInfo] " + url + "#" + res );


            try
            {
                UserInfoResultModel wx_token = JsonUtil.FromJson<UserInfoResultModel>( res );
                if ( wx_token != null && wx_token.errcode == 0 )
                {
                    return wx_token;
                }
                else
                {
                    Log.Logger.Log( "[wx: GetUserInfo Failed] " + url + "#" + res );
                    return null;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: GetUserInfo Exception] " + url + "#" + ex.Message );
                return null;
            }
        }


    }
}
