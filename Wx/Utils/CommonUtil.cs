using AppUtils;
using System;
using System.Collections.Generic;
using Wx.Utils.Model;

namespace Wx.Utils
{
    public class CommonUtil
    {
        /// <summary>
        /// 获取微信服务器的IP列表
        /// </summary>
        /// <param name="token">access token</param>
        /// <returns></returns>
        public static List<string> WxIPList( string token )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/getcallbackip";
            url += "?access_token=" + token;

            var res = new HttpUtil( ).Get( url );

            Log.Logger.Log( "[wx: WxIPList] " + url + "#" + res );

            try
            {
                IPListModel wx_iplist = JsonUtil.FromJson<IPListModel>( res );
                if ( wx_iplist != null && wx_iplist.errcode == 0 )
                {
                    return wx_iplist.ip_list;
                }
                else
                {
                    Log.Logger.Log( "[wx: WxIPList Failed] " + url + "#" + res );
                    return null;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: WxIPList Exception] " + url + "#" + ex.Message );
                return null;
            }
        }



        /// <summary>
        /// 长连接转短链接
        /// 使用场景：将生成二维码的长连接转成短链接之后，再生成二维码，可提高扫码速度和成功率
        /// </summary>
        /// <param name="token"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ShortURL( string token, string long_url )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/shorturl";
            url += "?access_token=" + token;

            var postdata = new
            {
                action = "long2short",
                long_url = long_url,
            };

            var res = new AppUtils.HttpUtil( ).PostJson( url, AppUtils.JsonUtil.ToJson( postdata ) );

            Log.Logger.Log( "[wx: 长连接转短链接] " + url + "#" + res );

            try
            {
                ShortURLResultModel wx_shorturl = JsonUtil.FromJson<ShortURLResultModel>( res );
                if ( wx_shorturl != null && wx_shorturl.errcode == 0 )
                {
                    return wx_shorturl.short_url;
                }
                else
                {
                    Log.Logger.Log( "[wx: 长连接转短链接 Failed] " + url + "#" + res );
                    return null;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: 长连接转短链接 Exception] " + url + "#" + ex.Message );
                return null;
            }
        }



        /// <summary>
        /// 获取永久二维码ticket
        /// </summary>
        /// <param name="token"></param>
        /// <param name="sceneId">场景值</param>
        /// <returns></returns>
        public static GetTicketResultModel GetQRCodeTicket( string token, string sceneId )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/qrcode/create";
            url += "?access_token=" + token;

            var postdata = new
            {
                action_name = "QR_LIMIT_STR_SCENE",
                action_info = new
                {
                    scene = new
                    {
                        scene_str = sceneId,
                    }
                },
            };

            var res = new AppUtils.HttpUtil( ).PostJson( url, AppUtils.JsonUtil.ToJson( postdata ) );

            Log.Logger.Log( "[wx: 获取永久二维码] " + url + "#" + res );

            try
            {
                GetTicketResultModel wx_ticket = JsonUtil.FromJson<GetTicketResultModel>( res );
                if ( wx_ticket != null && wx_ticket.errcode == 0 )
                {
                    return wx_ticket;
                }
                else
                {
                    Log.Logger.Log( "[wx: 获取永久二维码 Failed] " + url + "#" + res );
                    return null;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: 获取永久二维码 Exception] " + url + "#" + ex.Message );
                return null;
            }
        }


        


    }
}
