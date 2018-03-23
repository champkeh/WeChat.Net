using Wx.MessageManagement.Normal.Models.Events;
using Wx.MessageManagement.Normal.MsgHandlers;

namespace Wx.MessageManagement.Normal.EventHandlers
{
    class ViewEventHandler : IWxMsgHandler
    {
        public string Handle( string msgStr )
        {
            var msg = Xml.Net.XmlConvert.DeserializeObject<ClickMenuRedirectEvent>( msgStr );

            Log.Logger.Log( "[wx: view事件] 用户" + msg.FromUserName + "点击了自定义菜单: " + msg.EventKey );
            return null;
        }
    }
}
