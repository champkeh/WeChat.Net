namespace Wx.MessageManagement.Normal.Models.Events
{
    /// <summary>
    /// 事件基类
    /// todo: 下个版本实现事件处理
    /// </summary>
    public class EventMessage
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public int CreateTime { get; set; }
        public string MsgType { get; set; }
        public string Event { get; set; }
        public string EventKey { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    class ClickMenuPullEvent
    {
    }


    class ClickMenuRedirectEvent
    {
    }

    class LocationEvent
    {
    }

    class ScanEvent
    {
    }

    class ScanWithoutSubscribeEvent
    {
    }

    class SubscribeEvent
    {
    }

    class UnsubscribeEvent
    {
    }
}
