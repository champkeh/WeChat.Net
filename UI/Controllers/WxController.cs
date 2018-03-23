using Extensions;
using System.Threading.Tasks;
using System.Web.Mvc;
using UI.Attributes;
using Wx.Utils.Crypto;

namespace UI.Controllers
{
    /// <summary>
    /// 接入开发者模式
    /// 
    /// 此控制器实现了以下功能：
    ///   1. 验证开发者Token
    ///       GET /wx/access?signature=&echostr=&timestamp=&nonce=
    ///   2. 明文模式的消息处理
    ///       POST /wx/access?signature=&timestamp=&nonce=&openid=
    ///   3. 安全模式的消息处理
    ///       POST /wx/access?signature=&timestamp=&nonce=&openid=&encrypt_type=aes&msg_signature=
    /// </summary>
    [RoutePrefix("wx")]
    public class WxController : Controller
    {

        /// <summary>
        /// 接入开发者模式
        /// </summary>
        /// <param name="signature">消息签名，用于验证Token</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符串</param>
        /// <param name="echostr">验证成功后，原样返回该值</param>
        /// <returns></returns>
        [HttpGet]
        [Route( "access" )]
        [RequiresParameter( "echostr" )]
        public string AccessGet( string signature, string echostr, string timestamp, string nonce )
        {
            // 验证Token
            if ( Wx.Utils.TokenVerifyUtil.VerifySign( signature, timestamp, nonce ) )
            {
                Log.Logger.Log( "[wx: 开发者模式接入成功]" );
                return echostr;
            }

            Log.Logger.Log( "[wx: 开发者模式接入失败] Token验证失败" );
            return "Welcome to JingZhi.";
        }




        /// <summary>
        /// 接收微信服务器推送的消息和事件
        /// 消息加解密方式： 明文模式
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        [HttpPost]
        [Route( "access" )]
        [RequiresParameter( "openid" )]
        [NoParameter( "encrypt_type" )]
        [NoParameter( "msg_signature" )]
        public ActionResult AccessPost( string signature, string timestamp, string nonce, string openid )
        {
            // 验证Token
            if ( !Wx.Utils.TokenVerifyUtil.VerifySign( signature, timestamp, nonce ) )
            {
                Log.Logger.Log( "[wx: 消息签名验证失败]" );
                return Content( "你的ip已被记录，警察稍候会请你喝茶，好自为之~" );
            }

            // post数据（明文）
            string postDataStr = System.Web.HttpContext.Current.Request.InputStream.ReadAllString( );


            // 异步处理消息。如果需要回复，则采用客服接口异步回复
            Task.Run( () =>
             {
                 // 记录日志
                 Log.Logger.Log( "[wx: 收到消息] " + postDataStr );


                 string reply = string.Empty;
                 try
                 {
                     // 解析并执行
                     reply = Wx.MessageManagement.MessageParser.ParseAndExecute( postDataStr );
                 }
                 catch ( System.NotSupportedException ex )
                 {
                     Log.Logger.Log( "[wx: 处理消息 异常] " + ex.Message );
                     return;
                 }
                 catch ( System.Exception ex )
                 {
                     Log.Logger.Log( "[wx: 处理消息 异常] " + ex.Message );
                     return;
                 }

                 // 获取token
                 var token = Wx.Utils.AccessTokenUtil.GetAccessToken( );

                 // 调用客服消息接口进行异步回复
                 var ret = Wx.MessageManagement.Service.ServiceUtil.Send( token, reply );
                 if ( ret == false )
                 {
                     Log.Logger.Log( "[wx: 客服消息发送失败]" );
                     return;
                 }
             } );


            return Content( "success" );
        }




        /// <summary>
        /// 接收微信服务器推送的消息和事件
        /// 消息加解密方式： 安全模式
        /// </summary>
        /// <param name="signature">消息签名，用于验证Token</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机串</param>
        /// <param name="encrypt_type">加解密算法，当前为aes</param>
        /// <param name="msg_signature">消息签名，用于验证加解密</param>
        /// <returns></returns>
        [HttpPost]
        [Route( "access" )]
        [RequiresParameter( "openid" )]
        [RequiresParameter( "encrypt_type" )]
        [RequiresParameter( "msg_signature" )]
        public ActionResult AccessPost( string signature, string timestamp, string nonce, string openid, string encrypt_type, string msg_signature )
        {
            // 验证Token
            if ( !Wx.Utils.TokenVerifyUtil.VerifySign( signature, timestamp, nonce ) )
            {
                Log.Logger.Log( "[wx: 消息签名验证失败]" );
                return Content( "你的ip已被记录，警察稍候会请你喝茶，好自为之~" );
            }

            // post数据（密文）
            string postDataStr = System.Web.HttpContext.Current.Request.InputStream.ReadAllString( );

            // 记录日志
            Log.Logger.Log( "[wx: 收到消息] " + postDataStr );


            // 首先进行消息解密
            string rawDataStr = "";
            WXBizMsgCrypt wxcpt = new WXBizMsgCrypt( Wx.Config.Token, Wx.Config.EncodingAESKey, Wx.Config.AppID );
            var deCode = wxcpt.DecryptMsg( msg_signature, timestamp, nonce, postDataStr, ref rawDataStr );
            if ( deCode != 0 )
            {
                Log.Logger.Log( "[wx: 消息解密失败] 错误码: " + deCode );
                return Content( "" );
            }



            // 异步处理消息。如果需要回复，则采用客服接口异步回复
            Task.Run( () =>
            {
                // 记录日志
                Log.Logger.Log( "[wx: 消息解密成功] " + rawDataStr );


                string reply = string.Empty;
                try
                {
                    // 解析并执行
                    reply = Wx.MessageManagement.MessageParser.ParseAndExecute( rawDataStr );
                }
                catch ( System.NotSupportedException ex )
                {
                    Log.Logger.Log( "[wx: 处理消息 异常] " + ex.Message );
                    return;
                }
                catch ( System.Exception ex )
                {
                    Log.Logger.Log( "[wx: 处理消息 异常] " + ex.Message );
                    return;
                }

                // 获取token
                var token = Wx.Utils.AccessTokenUtil.GetAccessToken( );

                // 调用客服消息接口进行异步回复
                // 此处不需要对消息进行加密
                var ret = Wx.MessageManagement.Service.ServiceUtil.Send( token, reply );
                if ( ret == false )
                {
                    Log.Logger.Log( "[wx: 客服消息发送失败]" );
                    return;
                }
            } );


            return Content( "success" );
        }

    }
}