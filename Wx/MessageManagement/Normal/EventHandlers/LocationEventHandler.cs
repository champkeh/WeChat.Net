using Wx.MessageManagement.Normal.Models.Events;
using Wx.MessageManagement.Normal.MsgHandlers;

namespace Wx.MessageManagement.Normal.EventHandlers
{
    class LocationEventHandler : IWxMsgHandler
    {
        public string Handle( string msgStr )
        {
            var msg = Xml.Net.XmlConvert.DeserializeObject<LocationEvent>( msgStr );

            Log.Logger.Log( "[wx: location事件] 用户" + msg.FromUserName + "上报了地理位置: (" + msg.Latitude + "," + msg.Longitude + "," + msg.Precision + ")" );
            return null;
        }
    }
}
