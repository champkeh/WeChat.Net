using BIZService.Models;

namespace BIZService
{
    public class MsgNotify
    {
        private static void send( string templateId, string openid, string url, string first, string k1, string k2, string k3, string remark )
        {
            WxNotifyParam p = new WxNotifyParam( )
            {
                template_id = templateId,
                touser = openid,
                url = url,
                data = new NotifyParams
                {
                    first = new NotifyParamItem
                    {
                        value = first,
                        color = "#173177",
                    },
                    keyword1 = new NotifyParamItem
                    {
                        value = k1,
                        color = "#173177",
                    },
                    keyword2 = new NotifyParamItem
                    {
                        value = k2,
                        color = "#173177",
                    },
                    keyword3 = new NotifyParamItem
                    {
                        value = k3,
                        color = "#173177",
                    },
                    remark = new NotifyParamItem
                    {
                        value = remark,
                        color = "#173177",
                    },
                },
            };

            var token = Wx.Utils.AccessTokenUtil.GetAccessToken( );
            Wx.MessageManagement.Template.TemplateManager.Send( token, AppUtils.JsonUtil.ToJson( p ) );
        }





        /// <summary>
        /// 订单生成通知
        /// </summary>
        /// <param name="openid">发送对象的openid</param>
        /// <param name="url">url</param>
        /// <param name="first">消息内容</param>
        /// <param name="k1">时间</param>
        /// <param name="k2">商品名称</param>
        /// <param name="k3">订单号</param>
        /// <param name="remark">备注</param>
        public static void OrderCompleteNotify( string openid, string url, string first, string k1, string k2, string k3, string remark )
        {
            var tmpl = DataAccess.WxTemplateDAL.Get( Wx.Config.AppID, "kVsgiOB4ROw7LGMVzsNS-TVFNq3wuVDwxZbzojcPU2k" );
            if ( tmpl == null )
            {
                Log.Logger.Log( "[biz: 消息发送失败] " + "找不到订单生成模板" );
                return;
            }

            send( tmpl.template_id, openid, url, first, k1, k2, k3, remark );
        }




        /// <summary>
        /// 登机核验提醒
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="url"></param>
        /// <param name="first"></param>
        /// <param name="k1"></param>
        /// <param name="k2"></param>
        /// <param name="k3"></param>
        /// <param name="remark"></param>
        public static void FlightCheckNotify( string openid, string url, string first, string k1, string k2, string k3, string remark )
        {
            var tmpl = DataAccess.WxTemplateDAL.Get( Wx.Config.AppID, "1LldCWCe92u6Pe_zLtGMgwjUsh7njot5RUnyGkKjxX4" );
            if ( tmpl == null )
            {
                Log.Logger.Log( "[biz: 消息发送失败] " + "找不到登机核验提醒模板" );
                return;
            }

            send( tmpl.template_id, openid, url, first, k1, k2, k3, remark );
        }



    }
}
