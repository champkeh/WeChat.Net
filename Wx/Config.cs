namespace Wx
{
    public static class Config
    {
        /// <summary>
        /// 公众号appid
        /// </summary>
        public static string AppID { get; set; }

        /// <summary>
        /// 公众号appsecret
        /// </summary>
        public static string AppSecret { get; set; }

        /// <summary>
        /// 接入开发者token
        /// </summary>
        public static string Token { get; set; }

        /// <summary>
        /// 商户id
        /// </summary>
        public static string MchID { get; set; }

        /// <summary>
        /// api密钥
        /// </summary>
        public static string ApiKey { get; set; }

        /// <summary>
        /// api证书路径
        /// </summary>
        public static string CertPath { get; set; }

        /// <summary>
        /// 消息加解密密钥
        /// </summary>
        public static string EncodingAESKey { get; set; }


        /// <summary>
        /// 授权回调域名
        /// </summary>
        public static string Domain { get; set; }


        static Config()
        {
            AppID = AppUtils.ConfigUtil.GetConfigString( "AppId" );
            AppSecret = AppUtils.ConfigUtil.GetConfigString( "AppSecret" );
            MchID = AppUtils.ConfigUtil.GetConfigString( "MchId" );
            ApiKey = AppUtils.ConfigUtil.GetConfigString( "ApiKey" );
            CertPath = AppUtils.ConfigUtil.GetConfigString( "ApiCert" );
            Token = AppUtils.ConfigUtil.GetConfigString( "Token" );
            EncodingAESKey = AppUtils.ConfigUtil.GetConfigString( "EncodingAESKey" );

            Domain = AppUtils.ConfigUtil.GetConfigString( "domain" );
        }
    }


}
