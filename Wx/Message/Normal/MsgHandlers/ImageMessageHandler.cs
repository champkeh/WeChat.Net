namespace Wx.Message.Normal.MsgHandlers
{
    /// <summary>
    /// 图片消息处理器
    /// </summary>
    public class ImageMessageHandler : IWxMsgHandler
    {

        /// <summary>
        /// 处理图片消息
        /// </summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public string Handle( string msgStr )
        {
            var imgMsg = Xml.Net.XmlConvert.DeserializeObject<Models.Messages.ImageMessage>( msgStr );


            // 构造客服回复消息
            var reply = new Service.Models.TextMessage
            {
                touser = imgMsg.FromUserName,
                text = new Service.Models.TextContent
                {
                    content = "收到了你的图片消息: " + imgMsg.PicUrl,
                },
            };


            return AppUtils.JsonUtil.ToJson( reply );
        }
    }
}
