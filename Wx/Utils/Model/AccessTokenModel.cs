namespace Wx.Utils.Model
{

    /// <summary>
    /// 接口调用凭据
    /// </summary>
    public class AccessTokenModel : WxError
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }
    }


   
}
