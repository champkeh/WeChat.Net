using Wx.MessageManagement.Normal.Models.Events;
using Wx.MessageManagement.Normal.MsgHandlers;

namespace Wx.MessageManagement.Normal.EventHandlers
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
