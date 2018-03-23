namespace Wx.MessageManagement.Normal.MsgHandlers
{
    /// <summary>
    /// 普通文本消息处理器
    /// 用于处理用户发送给公众号的文本消息
    /// </summary>
    public class TextMessageHandler : IWxMsgHandler
    {
        /// <summary>
        /// 处理文本消息
        /// </summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public string Handle( string msgStr )
        {
            var textMsg = Xml.Net.XmlConvert.DeserializeObject<Models.Messages.TextMessage>( msgStr );


            // 构造客服回复消息
            var reply = new Service.Models.TextMessage
            {
                touser = textMsg.FromUserName,
                text = new Service.Models.TextContent
                {
                    content = "收到了你的文本消息: " + textMsg.Content,
                },
            };


            return AppUtils.JsonUtil.ToJson( reply );
        }
    }
}
