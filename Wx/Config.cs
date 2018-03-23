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

        static Config()
        {
            AppID = AppUtils.ConfigHelper.GetConfigString( "AppId" );
            AppSecret = AppUtils.ConfigHelper.GetConfigString( "AppSecret" );
            MchID = AppUtils.ConfigHelper.GetConfigString( "MchId" );
            ApiKey = AppUtils.ConfigHelper.GetConfigString( "ApiKey" );
            CertPath = AppUtils.ConfigHelper.GetConfigString( "ApiCert" );
            Token = AppUtils.ConfigHelper.GetConfigString( "Token" );
            EncodingAESKey = AppUtils.ConfigHelper.GetConfigString( "EncodingAESKey" );
        }
    }


}
