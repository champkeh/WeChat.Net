using System;

namespace Wx.Message.Normal.MsgHandlers
{
    /// <summary>
    /// 音频消息处理器
    /// </summary>
    public class VoiceMessageHandler : IWxMsgHandler
    {
        /// <summary>
        /// 处理音频消息
        /// </summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public string Handle( string msgStr )
        {
            var vMsg = Xml.Net.XmlConvert.DeserializeObject<Models.Messages.VoiceMessage>( msgStr );


            // 构造客服回复消息
            var reply = new Service.Models.TextMessage
            {
                touser = vMsg.FromUserName,
                text = new Service.Models.TextContent
                {
                    content = "收到了你的音频消息: " + vMsg.Format,
                },
            };


            return AppUtils.JsonUtil.ToJson( reply );
        }
    }
}
