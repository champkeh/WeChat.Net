using System.Collections.Generic;

namespace Wx.Message.Service.Models
{
    /// <summary>
    /// 客服消息基类
    /// </summary>
    public class BaseMessage
    {
        /// <summary>
        /// 用户的openid
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string msgtype { get; set; }
    }



    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMessage : BaseMessage
    {
        public TextContent text { get; set; }

        public TextMessage()
        {
            msgtype = "text";
        }
    }
    public class TextContent
    {
        public string content { get; set; }
    }

    public class MediaContent
    {
        public string media_id { get; set; }
    }



    /// <summary>
    /// 图片消息
    /// </summary>
    public class ImageMessage : BaseMessage
    {
        public MediaContent image { get; set; }
        public ImageMessage()
        {
            msgtype = "image";
        }
    }




    /// <summary>
    /// 语音消息
    /// </summary>
    public class VoiceMessage : BaseMessage
    {
        public MediaContent voice { get; set; }
        public VoiceMessage()
        {
            msgtype = "voice";
        }
    }




    /// <summary>
    /// 视频消息
    /// </summary>
    public class VideoMessage : BaseMessage
    {
        public VideoContent video { get; set; }
        public VideoMessage()
        {
            msgtype = "video";
        }
    }
    public class VideoContent
    {
        public string media_id { get; set; }
        public string thumb_media_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }





    /// <summary>
    /// 音乐消息
    /// </summary>
    public class MusicMessage : BaseMessage
    {
        public MusicContent music { get; set; }
        public MusicMessage()
        {
            msgtype = "music";
        }
    }
    public class MusicContent
    {
        public string title { get; set; }
        public string description { get; set; }
        public string musicurl { get; set; }
        public string hqmusicurl { get; set; }
        public string thumb_media_id { get; set; }
    }





    /// <summary>
    /// 图文消息
    /// </summary>
    public class NewsMessage : BaseMessage
    {
        public NewsContent news { get; set; }
        public NewsMessage()
        {
            msgtype = "news";
        }
    }
    public class NewsContent
    {
        public List<Article> articles { get; set; }

        public NewsContent()
        {
            articles = new List<Article>( );
        }
    }
    public class Article
    {
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string picurl { get; set; }
    }




    /// <summary>
    /// 图文消息2
    /// </summary>
    public class MpNewsMessage : BaseMessage
    {
        public MediaContent mpnews { get; set; }
        public MpNewsMessage()
        {
            msgtype = "mpnews";
        }
    }



    /// <summary>
    /// 卡券消息
    /// </summary>
    public class CardMessage : BaseMessage
    {
        public CardContent wxcard { get; set; }
        public CardMessage()
        {
            msgtype = "wxcard";
        }
    }
    public class CardContent
    {
        public string card_id { get; set; }
    }


}
