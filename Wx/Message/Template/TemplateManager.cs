using AppUtils;
using System;
using System.Linq;
using System.Collections.Generic;
using Wx.Message.Template.Models;

namespace Wx.Message.Template
{
    public class TemplateManager
    {
        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="msg">json字符串</param>
        /// <returns></returns>
        public static bool Send( string token, string msg )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/template/send";
            url += "?access_token=" + token;

            var res = new HttpUtil( ).PostJson( url, msg );

            Log.Logger.Log( "[wx: Send发送模板消息] " + url + "#" + res );

            try
            {
                TemplateMsgResult wx_tmpl_res = JsonUtil.FromJson<TemplateMsgResult>( res );
                if ( wx_tmpl_res != null && wx_tmpl_res.errcode == 0 )
                {
                    return true;
                }
                else
                {
                    Log.Logger.Log( "[wx: Send发送模板消息 失败] " + url + "#" + res );
                    return false;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: Send发送模板消息 错误] " + url + "#" + ex.Message );
                return false;
            }
        }



        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="token"></param>
        public static List<TemplateItem> List( string token )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/template/get_all_private_template";
            url += "?access_token=" + token;

            var res = new HttpUtil( ).Get( url );

            Log.Logger.Log( "[wx: 获取模板列表] " + url + "#" + res );

            try
            {
                TemplateListResult wx_tmpl_list = JsonUtil.FromJson<TemplateListResult>( res );
                if ( wx_tmpl_list != null && wx_tmpl_list.errcode == 0 )
                {
                    return wx_tmpl_list.template_list;
                }
                else
                {
                    Log.Logger.Log( "[wx: 获取模板列表 失败] " + url + "#" + res );
                    return null;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: 获取模板列表 错误] " + url + "#" + ex.Message );
                return null;
            }
        }



        /// <summary>
        /// 更新数据库中的模板列表
        /// </summary>
        /// <param name="token"></param>
        public static void UpdateToDb( string token )
        {
            var list = List( token );
            if ( list == null )
            {
                return;
            }

            // 字段映射
            var model = list.Select( qu => new DataAccess.wx_templates
            {
                appid = Wx.Config.AppID,
                template_id = qu.template_id,
                title = qu.title,
                content = qu.content,
                primary_industry = qu.primary_industry,
                deputy_industry = qu.deputy_industry,
                example = qu.example,
            } ).ToList( );

            var ret = DataAccess.WxTemplateDAL.Update( Wx.Config.AppID, model );
            if ( ret )
            {
                Log.Logger.Log( "[wx: 更新模板库成功]" );
            }
            else
            {
                Log.Logger.Log( "[wx: 更新模板库失败]" );
            }
        }







    }
}
