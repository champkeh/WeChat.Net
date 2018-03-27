namespace Wx.Utils.Model
{
    public class GetTicketResultModel : WxError
    {
        public string ticket { get; set; }
        public int expire_seconds { get; set; }
        public string url { get; set; }
    }
}
