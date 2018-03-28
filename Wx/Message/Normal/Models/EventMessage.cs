namespace Wx.Message.Normal.Models.Events
{
    /// <summary>
    /// 事件基类
    /// </summary>
    public class EventMessageBase
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方openid
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public int CreateTime { get; set; }

        /// <summary>
        /// 消息类型，event
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public string Event { get; set; }

    }


    /// <summary>
    /// 自定义菜单click事件
    /// </summary>
    class ClickMenuPullEvent
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方openid
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public int CreateTime { get; set; }

        /// <summary>
        /// 消息类型，event
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 事件类型，click
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中的KEY值对应
        /// </summary>
        public string EventKey { get; set; }
    }


    /// <summary>
    /// 自定义菜单view事件
    /// </summary>
    class ClickMenuRedirectEvent
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方openid
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public int CreateTime { get; set; }

        /// <summary>
        /// 消息类型，event
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 事件类型，view
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        public string EventKey { get; set; }
    }


    /// <summary>
    /// 上报地理位置事件
    /// </summary>
    class LocationEvent
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方openid
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public int CreateTime { get; set; }

        /// <summary>
        /// 消息类型，event
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 事件类型，location
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public float Latitude { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public float Longitude { get; set; }

        /// <summary>
        /// 地理位置精度
        /// </summary>
        public float Precision { get; set; }
    }



    /// <summary>
    /// 关注事件/取关事件/扫码二维码事件
    /// </summary>
    class SubscribeEvent
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 关注者openid
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public int CreateTime { get; set; }

        /// <summary>
        /// 消息类型，event
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 事件类型，subscribe/unsubscribe
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }
    }

}
