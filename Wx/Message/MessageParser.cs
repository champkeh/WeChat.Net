using Wx.Message.Normal.MsgHandlers;
using Wx.Message.Normal.Models.Messages;
using Wx.Message.Normal.Models.Events;
using Wx.Message.Normal.EventHandlers;

namespace Wx.Message
{
    public class MessageParser
    {
        /// <summary>
        /// 解析消息并执行相应逻辑
        /// </summary>
        /// <param name="msgStr">微信post过来的消息数据，xml格式</param>
        /// <returns>回复给用户的消息，将使用客服接口进行回复</returns>
        /// <exception cref="NotSupportedException"></exception>
        public static string ParseAndExecute( string msgStr )
        {
            var msgbase = Xml.Net.XmlConvert.DeserializeObject<MessageBase>( msgStr );


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
                    return ParseEvent( msgStr );
                default:
                    throw new System.NotSupportedException( "不支持的消息类型: " + msgbase.MsgType );
            }

            return handler.Handle( msgStr );
        }



        /// <summary>
        /// 解析事件并执行相应逻辑
        /// </summary>
        /// <param name="msgStr">微信post过来的事件数据，xml格式</param>
        /// <returns>回复给用户的消息，将使用客服接口进行回复</returns>
        /// <exception cref="NotSupportedException"></exception>
        public static string ParseEvent( string msgStr )
        {
            var eventBase = Xml.Net.XmlConvert.DeserializeObject<EventMessageBase>( msgStr );

            IWxMsgHandler handler;


            switch ( eventBase.Event.ToLower() )
            {
                case "subscribe":
                    // 关注事件
                    // 分扫码关注和普通关注
                    handler = new SubscribeEventHandler( );
                    break;
                case "unsubscribe":
                    // 取关事件
                    handler = new UnsubscribeEventHandler( );
                    break;
                case "scan":
                    // 已关注用户扫描带参数二维码事件
                    handler = new ScanWithSubscribeEventHandler( );
                    break;
                case "location":
                    // 上报地理位置事件
                    handler = new LocationEventHandler( );
                    break;
                case "click":
                    // 自定义菜单click事件
                    handler = new ClickEventHandler( );
                    break;
                case "view":
                    // 自定义菜单view事件
                    handler = new ViewEventHandler( );
                    break;
                default:
                    // todo: 实现更多事件处理，比如自定义菜单的事件
                    throw new System.NotSupportedException( "不支持的事件类型: " + eventBase.Event );
            }

            return handler.Handle( msgStr );
        }



    }
}
