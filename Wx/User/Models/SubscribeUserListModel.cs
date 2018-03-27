using System.Collections.Generic;

namespace Wx.User.Models
{
    public class SubscribeUserListModel : WxError
    {
        public int total { get; set; }
        public int count { get; set; }
        public SubscribeUserContentModel data { get; set; }
        public string next_openid { get; set; }
    }

    public class SubscribeUserContentModel
    {
        public List<string> openid { get; set; }
    }
}
