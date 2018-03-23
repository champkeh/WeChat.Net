using AppUtils;
using System;
using System.Collections.Generic;
using Wx.Menu.Models;

namespace Wx.Menu
{
    /// <summary>
    /// 自定义菜单管理器
    /// </summary>
    public class MenuManager
    {
        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static MenuModel Get( string token )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/menu/get";
            url += "?access_token=" + token;

            var res = new HttpUtil( ).Get( url );

            Log.Logger.Log( "[wx: GetMenu] " + url + "#" + res );

            try
            {
                MenuModel wx_menu = JsonUtil.FromJson<MenuModel>( res );
                if ( wx_menu != null && wx_menu.errcode == 0 )
                {
                    return wx_menu;
                }
                else
                {
                    Log.Logger.Log( "[wx: GetMenu Failed] " + url + "#" + res );
                    return null;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: GetMenu Exception] " + url + "#" + ex.Message );
                return null;
            }
        }



        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        public static bool Create( string token )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create";
            url += "?access_token=" + token;

            Models.Menu menu = new Models.Menu
            {
                button = new List<Button>
                {
                    new Button()
                    {
                        type = "click",
                        name = "今日歌曲",
                        key = "V1001_TODAY_MUSIC",
                    },
                    new Button
                    {
                        name = "菜单",
                        sub_button = new List<Button>
                        {
                            new Button
                            {
                                type = "view",
                                name = "搜索",
                                url = "http://www.soso.com/",
                            },
                            new Button
                            {
                                type = "click",
                                name = "赞一下我们",
                                key = "V1001_GOOD",
                            },
                        },
                    },
                },
            };
            var json = JsonUtil.ToJson( menu );


            var res = new HttpUtil( ).PostJson( url, json );

            Log.Logger.Log( "[wx: CreateMenu] " + url + "#" + res );

            try
            {
                WxError wx_menu = JsonUtil.FromJson<WxError>( res );
                if ( wx_menu != null && wx_menu.errcode == 0 )
                {
                    return true;
                }
                else
                {
                    Log.Logger.Log( "[wx: CreateMenu Failed] " + url + "#" + res );
                    return false;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: CreateMenu Exception] " + url + "#" + ex.Message );
                return false;
            }
        }



        /// <summary>
        /// 删除自定义菜单
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool Delete( string token )
        {
            string url = "https://api.weixin.qq.com/cgi-bin/menu/delete";
            url += "?access_token=" + token;

            var res = new HttpUtil( ).Get( url );

            Log.Logger.Log( "[wx: DeleteMenu] " + url + "#" + res );

            try
            {
                WxError wx_menu = JsonUtil.FromJson<WxError>( res );
                if ( wx_menu != null && wx_menu.errcode == 0 )
                {
                    return true;
                }
                else
                {
                    Log.Logger.Log( "[wx: DeleteMenu Failed] " + url + "#" + res );
                    return false;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[wx: DeleteMenu Exception] " + url + "#" + ex.Message );
                return false;
            }
        }


        //todo: 实现个性化菜单接口

    }
}
