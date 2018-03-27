using AppUtils;
using System;
using System.Collections.Generic;
using Wx.User.Models;
using System.Linq;

namespace Wx.User
{
    public class UserManager
    {
        /// <summary>
        /// 关注者列表，每次最多拉取10000人
        /// </summary>
        /// <param name="token"></param>
        /// <param name="next_openid"></param>
        /// <returns></returns>
        public static SubscribeUserListModel SubscribeList( string token, string next_openid )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/user/get";
            url += "?access_token=" + token;
            url += "&next_openid=" + next_openid;


            var res = new AppUtils.HttpUtil( ).Get( url );

            Log.Logger.Log( "[wx: SubscribeList] " + url + "#" + res );

            try
            {
                SubscribeUserListModel wx_userlist = JsonUtil.FromJson<SubscribeUserListModel>( res );
                if ( wx_userlist != null && wx_userlist.errcode == 0 )
                {
                    return wx_userlist;
                }
                else
                {
                    Log.Logger.Log( "[wx: SubscribeList Failed] " + url + "#" + res );
                    return null;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: SubscribeList Exception] " + url + "#" + ex.Message );
                return null;
            }
        }



        /// <summary>
        /// 获取关注者的基本信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid"></param>
        public static SubscribeUserInfoModel SubscribeUserInfo( string token, string openid )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/user/info";
            url += "?access_token=" + token;
            url += "&openid=" + openid;
            url += "&lang=zh_CN";


            var res = new AppUtils.HttpUtil( ).Get( url );

            Log.Logger.Log( "[wx: SubscribeUserInfo] " + url + "#" + res );

            try
            {
                SubscribeUserInfoModel wx_userinfo = JsonUtil.FromJson<SubscribeUserInfoModel>( res );
                if ( wx_userinfo != null && wx_userinfo.errcode == 0 )
                {
                    return wx_userinfo;
                }
                else
                {
                    Log.Logger.Log( "[wx: SubscribeUserInfo Failed] " + url + "#" + res );
                    return null;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: SubscribeUserInfo Exception] " + url + "#" + ex.Message );
                return null;
            }
        }



        /// <summary>
        /// 批量获取用户基本信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openids"></param>
        public static BatchSubscribeUserInfoModel BatchGetUserInfo( string token, List<string> openids )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/user/info/batchget";
            url += "?access_token=" + token;

            var list = new
            {
                user_list = openids.Select( qu => new
                {
                    openid = qu,
                    lang = "zh_CN",
                } ).ToList( )
            };

            var json = AppUtils.JsonUtil.ToJson( list );

            var res = new AppUtils.HttpUtil( ).PostJson( url, json );

            Log.Logger.Log( "[wx: BatchGetUserInfo] " + url + "#" + res );

            try
            {
                BatchSubscribeUserInfoModel wx_userinfo = JsonUtil.FromJson<BatchSubscribeUserInfoModel>( res );
                if ( wx_userinfo != null && wx_userinfo.errcode == 0 )
                {
                    return wx_userinfo;
                }
                else
                {
                    Log.Logger.Log( "[wx: BatchGetUserInfo Failed] " + url + "#" + res );
                    return null;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: BatchGetUserInfo Exception] " + url + "#" + ex.Message );
                return null;
            }

        }


    }
}
