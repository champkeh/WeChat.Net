using Wx.MessageManagement.Normal.Models.Events;
using Wx.MessageManagement.Normal.MsgHandlers;

namespace Wx.MessageManagement.Normal.EventHandlers
{
    class ScanWithSubscribeEventHandler : IWxMsgHandler
    {
        public string Handle( string msgStr )
        {
            var msg = Xml.Net.XmlConvert.DeserializeObject<SubscribeEvent>( msgStr );


            Log.Logger.Log( "[wx: 扫码事件] 用户" + msg.FromUserName + "扫描了带参数二维码，渠道是扫描二维码关注，二维码参数: " + msg.EventKey + "#" + msg.Ticket );

            return null;
        }
    }
}
