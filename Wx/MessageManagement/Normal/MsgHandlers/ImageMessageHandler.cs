using System;

namespace Wx.MessageManagement.Normal.MsgHandlers
{
    /// <summary>
    /// 图片消息处理器
    /// </summary>
    public class ImageMessageHandler : IWxMsgHandler
    {

        /// <summary>
        /// 处理图片消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string Handle( string msg )
        {
            var imgMsg = Xml.Net.XmlConvert.DeserializeObject<Models.Messages.ImageMessage>( msg );


            // 构造客服回复消息
            Wx.MessageManagement.Service.Models.ImageMessage reply = new Service.Models.ImageMessage
            {
                touser = imgMsg.FromUserName,
                image = new Service.Models.MediaContent
                {
                    media_id = imgMsg.MediaId,
                },
            };


            return AppUtils.JsonUtil.ToJson( reply );
        }
    }
}
