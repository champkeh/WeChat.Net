using System;

namespace Wx.MessageManagement.Normal.MsgHandlers
{
    /// <summary>
    /// 链接消息处理器
    /// </summary>
    class LinkMessageHandler : IWxMsgHandler
    {
        /// <summary>
        /// 处理链接消息
        /// </summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public string Handle( string msgStr )
        {
            var linkMsg = Xml.Net.XmlConvert.DeserializeObject<Models.Messages.LinkMessage>( msgStr );


            // 构造客服回复消息
            var reply = new Service.Models.TextMessage
            {
                touser = linkMsg.FromUserName,
                text = new Service.Models.TextContent
                {
                    content = "收到了你的链接消息: " + linkMsg.Url,
                },
            };


            return AppUtils.JsonUtil.ToJson( reply );
        }
    }
}
