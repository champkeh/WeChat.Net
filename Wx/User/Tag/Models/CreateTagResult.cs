namespace Wx.User.Tag.Models
{
    class CreateTagResult : WxError
    {
        public CreateTagInternalResult tag { get; set; }
    }

    class CreateTagInternalResult
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
