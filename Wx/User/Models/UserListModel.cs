using System.Collections.Generic;

namespace Wx.User.Models
{
    public class UserListModel : WxError
    {
        public int total { get; set; }
        public int count { get; set; }
        public UserContentModel data { get; set; }
        public string next_openid { get; set; }
    }

    public class UserContentModel
    {
        public List<string> openid { get; set; }
    }
}
