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
        public static List<string> WxIPList(string token)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/getcallbackip";
            url += "?access_token="+token;

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
    }
}
