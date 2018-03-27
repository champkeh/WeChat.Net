using AppUtils;
using System;
using Wx.User.Tag.Models;

namespace Wx.User.Tag
{
    public class TagManager
    {

        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name">标签名</param>
        /// <returns>是否创建成功</returns>
        public static bool Create( string token, string name )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/tags/create";
            url += "?access_token=" + token;

            //post { "tag" : { "name" : "广东"//标签名   } };
            var tag = new
            {
                tag = new
                {
                    name = name,
                }
            };
            var res = new AppUtils.HttpUtil( ).PostJson( url, AppUtils.JsonUtil.ToJson( tag ) );

            Log.Logger.Log( "[wx: CreateTag] " + url + "#" + res );

            try
            {
                CreateTagResult wx_tag = JsonUtil.FromJson<CreateTagResult>( res );
                if ( wx_tag != null && wx_tag.errcode == 0 )
                {
                    return true;
                }
                else
                {
                    Log.Logger.Log( "[wx: CreateTag Failed] " + url + "#" + res );
                    return false;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: CreateTag Exception] " + url + "#" + ex.Message );
                return false;
            }
        }



        /// <summary>
        /// 获取tag列表
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static GetTagResult Get( string token )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/tags/get";
            url += "?access_token=" + token;


            var res = new AppUtils.HttpUtil( ).Get( url );

            Log.Logger.Log( "[wx: GetTag] " + url + "#" + res );

            try
            {
                GetTagResult wx_tag = JsonUtil.FromJson<GetTagResult>( res );
                if ( wx_tag != null && wx_tag.errcode == 0 )
                {
                    return wx_tag;
                }
                else
                {
                    Log.Logger.Log( "[wx: GetTag Failed] " + url + "#" + res );
                    return null;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: GetTag Exception] " + url + "#" + ex.Message );
                return null;
            }
        }



        //todo: 编辑标签


        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="token"></param>
        /// <param name="id">标签id</param>
        /// <returns>是否删除成功</returns>
        public static bool Delete( string token, int id )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/tags/delete";
            url += "?access_token=" + token;

            var tag = new
            {
                tag = new
                {
                    id = id
                }
            };

            var res = new AppUtils.HttpUtil( ).PostJson( url, AppUtils.JsonUtil.ToJson( tag ) );

            Log.Logger.Log( "[wx: DeleteTag] " + url + "#" + res );

            try
            {
                WxError wx_tag = JsonUtil.FromJson<WxError>( res );
                if ( wx_tag != null && wx_tag.errcode == 0 )
                {
                    return true;
                }
                else
                {
                    Log.Logger.Log( "[wx: DeleteTag Failed] " + url + "#" + res );
                    return false;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: DeleteTag Exception] " + url + "#" + ex.Message );
                return false;
            }
        }

    }
}
