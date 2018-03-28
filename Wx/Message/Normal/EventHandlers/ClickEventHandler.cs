using Wx.Message.Normal.Models.Events;
using Wx.Message.Normal.MsgHandlers;

namespace Wx.Message.Normal.EventHandlers
{
    class ClickEventHandler : IWxMsgHandler
    {
        public string Handle( string msgStr )
        {
            var msg = Xml.Net.XmlConvert.DeserializeObject<ClickMenuPullEvent>( msgStr );

            Log.Logger.Log( "[wx: click事件] 用户" + msg.FromUserName + "点击了自定义菜单: " + msg.EventKey );
            return null;
        }
    }
}
