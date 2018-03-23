namespace Wx.MessageManagement.Normal.MsgHandlers
{
    /// <summary>
    /// 视频消息处理器
    /// </summary>
    class VideoMessageHandler : IWxMsgHandler
    {
        /// <summary>
        /// 处理视频消息
        /// </summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public string Handle( string msgStr )
        {
            var vMsg = Xml.Net.XmlConvert.DeserializeObject<Models.Messages.ShortVideoMessage>( msgStr );


            // 构造客服回复消息
            var reply = new Service.Models.TextMessage
            {
                touser = vMsg.FromUserName,
                text = new Service.Models.TextContent
                {
                    content = "收到了你的视频消息: " + vMsg.ThumbMediaId,
                },
            };


            return AppUtils.JsonUtil.ToJson( reply );
        }
    }
}
