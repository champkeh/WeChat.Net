using Wx.Message.Normal.Models.Events;
using Wx.Message.Normal.MsgHandlers;

namespace Wx.Message.Normal.EventHandlers
{
    /// <summary>
    /// 取消关注事件处理器
    /// </summary>
    class UnsubscribeEventHandler : IWxMsgHandler
    {
        public string Handle( string msgStr )
        {
            var msg = Xml.Net.XmlConvert.DeserializeObject<SubscribeEvent>( msgStr );

            Log.Logger.Log( "[wx: 取关事件] 用户" + msg.FromUserName + "取关了公众号" );
            return null;
        }
    }
}
