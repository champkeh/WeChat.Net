using System.Collections.Generic;

namespace Wx.User.Tag.Models
{
    public class GetTagResult : WxError
    {
        public List<GetTagInternalResult> tags { get; set; }

        public GetTagResult()
        {
            tags = new List<GetTagInternalResult>( );
        }
    }
    public class GetTagInternalResult
    {
        public int id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
    }
}
