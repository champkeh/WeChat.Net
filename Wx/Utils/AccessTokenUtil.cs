using AppUtils;
using System;
using Wx.Utils.Model;

namespace Wx.Utils
{
    public class AccessTokenUtil
    {
        /// <summary>
        /// 获取接口调用凭据
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/token";
            url += "?grant_type=client_credential";
            url += "&appid=" + Wx.Config.AppID;
            url += "&secret=" + Wx.Config.AppSecret;

            var res = new HttpUtil( ).Get( url );

            Log.Logger.Log( "[wx: GetAccessToken] " + url + "#" + res );

            try
            {
                AccessTokenModel wx_token = JsonUtil.FromJson<AccessTokenModel>( res );
                if ( wx_token != null && wx_token.errcode == 0 )
                {
                    return wx_token.access_token;
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
    }
}
