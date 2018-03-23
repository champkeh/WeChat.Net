using System;

namespace Wx.MessageManagement.Normal.MsgHandlers
{
    /// <summary>
    /// 短视频消息处理器
    /// </summary>
    class ShortVideoMessageHandler : IWxMsgHandler
    {
        /// <summary>
        /// 处理短视频消息
        /// </summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public string Handle( string msgStr )
        {
            var svMsg = Xml.Net.XmlConvert.DeserializeObject<Models.Messages.ShortVideoMessage>( msgStr );


            // 构造客服回复消息
            var reply = new Service.Models.TextMessage
            {
                touser = svMsg.FromUserName,
                text = new Service.Models.TextContent
                {
                    content = "收到了你的短视频消息: " + svMsg.ThumbMediaId,
                },
            };


            return AppUtils.JsonUtil.ToJson( reply );
        }
    }
}
