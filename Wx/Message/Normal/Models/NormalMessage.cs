using System;

namespace Wx.Message.Normal.Models.Messages
{
    /// <summary>
    /// 普通消息基类
    /// </summary>
    public class MessageBase
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public int CreateTime { get; set; }
        public string MsgType { get; set; }
        public Int64 MsgId { get; set; }
    }


    /// <summary>
    /// 图片消息
    /// todo: xml解析不支持从父类继承的字段，因此，暂时不用继承
    /// </summary>
    public class ImageMessage 
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public int CreateTime { get; set; }
        public string MsgType { get; set; }
        public Int64 MsgId { get; set; }
        public string PicUrl { get; set; }
        public string MediaId { get; set; }
    }


    /// <summary>
    /// 链接消息
    /// todo: xml解析不支持从父类继承的字段，因此，暂时不用继承
    /// </summary>
    public class LinkMessage
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public int CreateTime { get; set; }
        public string MsgType { get; set; }
        public Int64 MsgId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }


    /// <summary>
    /// 位置消息
    /// todo: xml解析不支持从父类继承的字段，因此，暂时不用继承
    /// </summary>
    public class LocationMessage
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public int CreateTime { get; set; }
        public string MsgType { get; set; }
        public Int64 MsgId { get; set; }
        public float Location_x { get; set; }
        public float Location_y { get; set; }
        public int Scale { get; set; }
        public string Label { get; set; }
    }



    /// <summary>
    /// 文本消息
    /// todo: xml解析不支持从父类继承的字段，因此，暂时不用继承
    /// </summary>
    public class TextMessage
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public int CreateTime { get; set; }
        public string MsgType { get; set; }
        public Int64 MsgId { get; set; }
        public string Content { get; set; }
    }


    /// <summary>
    /// 短视频消息
    /// todo: xml解析不支持从父类继承的字段，因此，暂时不用继承
    /// </summary>
    public class ShortVideoMessage
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public int CreateTime { get; set; }
        public string MsgType { get; set; }
        public Int64 MsgId { get; set; }
        public string MediaId { get; set; }
        public string ThumbMediaId { get; set; }
    }


    /// <summary>
    /// 视频消息
    /// todo: xml解析不支持从父类继承的字段，因此，暂时不用继承
    /// </summary>
    public class VideoMessage
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public int CreateTime { get; set; }
        public string MsgType { get; set; }
        public Int64 MsgId { get; set; }
        public string MediaId { get; set; }
        public string ThumbMediaId { get; set; }
    }


    /// <summary>
    /// 音频消息
    /// todo: xml解析不支持从父类继承的字段，因此，暂时不用继承
    /// </summary>
    public class VoiceMessage
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public int CreateTime { get; set; }
        public string MsgType { get; set; }
        public Int64 MsgId { get; set; }
        public string MediaId { get; set; }
        public string Format { get; set; }
    }


}
