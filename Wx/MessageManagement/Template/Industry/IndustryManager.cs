using AppUtils;
using System;

namespace Wx.MessageManagement.Template.Industry
{
    public class IndustryManager
    {
        public IndustryModel Get(string token )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/template/get_industry";
            url += "?access_token=" + token;

            var res = new HttpUtil( ).Get( url );

            Log.Logger.Log( "[wx: Get获取模板消息的行业信息] " + url + "#" + res );

            try
            {
                IndustryModel wx_industry = JsonUtil.FromJson<IndustryModel>( res );
                if ( wx_industry != null && wx_industry.errcode == 0 )
                {
                    return wx_industry;
                }
                else
                {
                    Log.Logger.Log( "[wx: Get获取模板消息的行业信息 失败] " + url + "#" + res );
                    return null;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: Get获取模板消息的行业信息 错误] " + url + "#" + ex.Message );
                return null;
            }
        }


        //public bool Set(string token )
        //{

        //}

    }
}
