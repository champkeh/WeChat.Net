using System.Collections.Generic;

namespace Wx.User.Models
{
    public class SubscribeUserInfoModel : WxError
    {
        /// <summary>
        /// 是否订阅该公众号。如果没有关注，则获取不到其余信息
        /// </summary>
        public bool subscribe { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }

        /// <summary>
        /// 用户性别。0未知，1男性，2女性
        /// </summary>
        public int sex { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }

        /// <summary>
        /// 用户头像
        /// 最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像）
        /// 用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
        /// </summary>
        public string headimgurl { get; set; }

        /// <summary>
        /// 用户关注时间，如果用户曾多次关注，则取最新的关注时间
        /// </summary>
        public long subscribe_time { get; set; }
        public string unionid { get; set; }

        /// <summary>
        /// 公众号对粉丝的备注
        /// </summary>
        public string remark { get; set; }
        public int groupid { get; set; }
        public List<int> tagid_list { get; set; }

        /// <summary>
        /// 用户关注的渠道来源
        /// ADD_SCENE_SEARCH 公众号搜索
        /// ADD_SCENE_ACCOUNT_MIGRATION 公众号迁移
        /// ADD_SCENE_PROFILE_CARD 名片分享
        /// ADD_SCENE_QR_CODE 扫描二维码
        /// ADD_SCENEPROFILE LINK 图文页内名称点击
        /// ADD_SCENE_PROFILE_ITEM 图文页右上角菜单
        /// ADD_SCENE_PAID 支付后关注
        /// ADD_SCENE_OTHERS 其他
        /// </summary>
        public string subscribe_scene { get; set; }
        public int qr_scene { get; set; }
        public string qr_scene_str { get; set; }


        public SubscribeUserInfoModel()
        {
            tagid_list = new List<int>( );
        }
    }
}
