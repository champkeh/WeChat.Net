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
                    new Button
                    {
                        name = "基本",
                        sub_button = new List<Button>
                        {
                            new Button
                            {
                                name = "今日热搜",
                                type = "click",
                                key = "secret_key",
                            },
                            new Button
                            {
                                name = "搜索",
                                type = "view",
                                url = "http://www.baidu.com/",
                            },
                        },
                    },
                    new Button()
                    {
                        name = "发图",
                        sub_button = new List<Button>
                        {
                            new Button
                            {
                                type = "pic_sysphoto",
                                name = "系统拍照发图",
                                key = "rselfmenu_1_0",
                            },
                            new Button
                            {
                                type = "pic_photo_or_album",
                                name = "拍照或者相册发图",
                                key = "rselfmenu_1_1",
                            },
                            new Button
                            {
                                type = "pic_weixin",
                                name = "微信相册发图",
                                key = "rselfmenu_1_2",
                            },
                        },
                    },
                    new Button
                    {
                        name = "扫码",
                        sub_button = new List<Button>
                        {
                            new Button
                            {
                                type = "scancode_waitmsg",
                                name = "扫码带提示",
                                key = "rselfmenu_0_0",
                            },
                            new Button
                            {
                                type = "scancode_push",
                                name = "扫码推事件",
                                key = "rselfmenu_0_1",
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
