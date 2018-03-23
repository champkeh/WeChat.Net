using System.Collections.Generic;

namespace Wx.Utils.Model
{
    public class IPListModel : WxError
    {
        public List<string> ip_list { get; set; }

        public IPListModel()
        {
            ip_list = new List<string>( );
        }
    }
}
