using System.Collections.Generic;

namespace Wx.User.Models
{
    public class BatchSubscribeUserInfoModel : WxError
    {
        public List<SubscribeUserInfoModel> user_info_list { get; set; }

        public BatchSubscribeUserInfoModel()
        {
            user_info_list = new List<SubscribeUserInfoModel>( );
        }
    }
}
