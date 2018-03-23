using Wx.MessageManagement.Normal.MsgHandlers;
using Wx.MessageManagement.Normal.Models.Messages;
using Wx.MessageManagement.Normal.Models.Events;

namespace Wx.MessageManagement
{
    public class MessageParser
    {
        /// <summary>
        /// 解析消息并执行响应逻辑
        /// </summary>
        /// <param name="msg"></param>
        /// <exception cref="NotSupportedException"></exception>
        public static string ParseAndExecute( string msg )
        {
            var msgbase = Xml.Net.XmlConvert.DeserializeObject<MessageBase>( msg );


            IWxMsgHandler handler;


            switch ( msgbase.MsgType )
            {
                case "text":
                    // 文本消息
                    handler = new TextMessageHandler( );
                    break;
                case "image":
                    // 图片消息
                    handler = new ImageMessageHandler( );
                    break;
                case "voice":
                    // 语言消息
                    handler = new VoiceMessageHandler( );
                    break;
                case "video":
                    // 视频消息
                    handler = new VideoMessageHandler( );
                    break;
                case "shortvideo":
                    // 小视频消息
                    handler = new ShortVideoMessageHandler( );
                    break;
                case "location":
                    // 地理位置消息
                    handler = new LocationMessageHandler( );
                    break;
                case "link":
                    // 链接消息
                    handler = new LinkMessageHandler( );
                    break;
                case "event":
                    // 事件
                    return ParseEvent( msg );
                default:
                    throw new System.NotSupportedException( "unsopport message type: " + msgbase.MsgType );
            }

            return handler.Handle( msg );
        }



        public static string ParseEvent( string msg )
        {
            var common = Xml.Net.XmlConvert.DeserializeObject<EventMessage>( msg );

            switch ( common.Event )
            {
                case "subscribe":
                    break;
                case "unsubscribe":
                    break;
                case "SCAN":
                    break;
                case "LOCATION":
                    break;
                case "CLICK":
                    break;
                case "VIEW":
                    break;
                default:
                    break;
            }

            return "";
        }



    }
}
