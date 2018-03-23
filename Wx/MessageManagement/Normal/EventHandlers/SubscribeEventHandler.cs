using Wx.MessageManagement.Normal.Models.Events;
using Wx.MessageManagement.Normal.MsgHandlers;

namespace Wx.MessageManagement.Normal.EventHandlers
{
    /// <summary>
    /// 关注事件处理器
    /// </summary>
    public class SubscribeEventHandler : IWxMsgHandler
    {
        public string Handle( string msgStr )
        {
            var msg = Xml.Net.XmlConvert.DeserializeObject<SubscribeEvent>( msgStr );

            if ( string.IsNullOrEmpty( msg.EventKey ) )
            {
                Log.Logger.Log( "[wx: 关注事件] 用户" + msg.FromUserName + "关注了公众号，渠道是搜索公众号进行关注" );
            }
            else
            {
                Log.Logger.Log( "[wx: 关注事件] 用户" + msg.FromUserName + "关注了公众号，渠道是扫描二维码关注，二维码参数: " + msg.EventKey + "#" + msg.Ticket );
            }

            return null;
        }
    }
}
