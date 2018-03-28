namespace Wx.Message.Normal.MsgHandlers
{
    /// <summary>
    /// 位置消息处理器
    /// </summary>
    class LocationMessageHandler : IWxMsgHandler
    {
        /// <summary>
        /// 处理位置消息
        /// </summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public string Handle( string msgStr )
        {
            var posMsg = Xml.Net.XmlConvert.DeserializeObject<Models.Messages.LocationMessage>( msgStr );


            // 构造客服回复消息
            var reply = new Service.Models.TextMessage
            {
                touser = posMsg.FromUserName,
                text = new Service.Models.TextContent
                {
                    content = "收到了你的位置消息: " + posMsg.Label,
                },
            };


            return AppUtils.JsonUtil.ToJson( reply );
        }
    }
}
