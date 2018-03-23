using AppUtils;
using System;

namespace Wx.MessageManagement.Service
{
    public class ServiceUtil
    {
        /// <summary>
        /// 发送客服消息
        /// </summary>
        /// <param name="token">access token</param>
        /// <param name="msg">消息内容</param>
        /// <returns>是否发送成功</returns>
        public static bool Send( string token, string msg )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/custom/send";
            url += "?access_token=" + token;

            var res = new HttpUtil( ).PostJson( url, msg );

            Log.Logger.Log( "[wx: Send发送客服消息] " + url + "#" + res );

            try
            {
                WxError wx_token = JsonUtil.FromJson<WxError>( res );
                if ( wx_token != null && wx_token.errcode == 0 )
                {
                    return true;
                }
                else
                {
                    Log.Logger.Log( "[wx: Send发送客服消息 失败] " + url + "#" + res );
                    return false;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: Send发送客服消息 错误] " + url + "#" + ex.Message );
                return false;
            }
        }
    }
}
