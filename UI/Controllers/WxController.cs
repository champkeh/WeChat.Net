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
    [RoutePrefix( "wx" )]
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
        /// <param name="signature">消息签名，用于验证Token</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符串</param>
        /// <param name="openid">用户openid</param>
        /// <returns></returns>
        [HttpPost]
        [Route( "access" )]
        [RequiresParameter( "openid" )]
        [NoParameter( "encrypt_type" )]
        [NoParameter( "msg_signature" )]
        public ActionResult AccessPost( string signature, string timestamp, string nonce, string openid )
        {
            var result = Do( signature, timestamp, nonce, openid, null, null );

            return Content( result );
        }




        /// <summary>
        /// 接收微信服务器推送的消息和事件
        /// 消息加解密方式： 安全模式
        /// </summary>
        /// <param name="signature">消息签名，用于验证Token</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符串</param>
        /// <param name="openid">用户openid</param>
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
            var result = Do( signature, timestamp, nonce, openid, encrypt_type, msg_signature );

            return Content( result );
        }




        /// <summary>
        /// 执行消息处理逻辑
        /// </summary>
        /// <param name="signature">消息签名，用于验证Token</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符串</param>
        /// <param name="openid">用户openid</param>
        /// <param name="encrypt_type">加解密算法，当前为aes</param>
        /// <param name="msg_signature">消息签名，用于验证加解密</param>
        /// <returns></returns>
        private string Do( string signature, string timestamp, string nonce, string openid, string encrypt_type, string msg_signature )
        {
            // 验证Token
            if ( !Wx.Utils.TokenVerifyUtil.VerifySign( signature, timestamp, nonce ) )
            {
                Log.Logger.Log( "[wx: 消息签名验证失败]" );
                return "你的ip已被记录，警察稍候会请你喝茶，好自为之~";
            }

            string rawMsgStr = "";

            // post数据
            string postMsgStr = System.Web.HttpContext.Current.Request.InputStream.ReadAllString( );
            if ( encrypt_type.ToLower( ) == "aes" && !string.IsNullOrEmpty( msg_signature ) )
            {
                // 消息解密
                WXBizMsgCrypt wxcpt = new WXBizMsgCrypt( Wx.Config.Token, Wx.Config.EncodingAESKey, Wx.Config.AppID );
                var deCode = wxcpt.DecryptMsg( msg_signature, timestamp, nonce, postMsgStr, ref rawMsgStr );
                if ( deCode != 0 )
                {
                    Log.Logger.Log( "[wx: 消息解密失败] 错误码: " + deCode );
                    return "消息解密失败";
                }
            }
            else
            {
                rawMsgStr = postMsgStr;
            }

            // 异步处理消息。如果需要回复，则采用客服接口异步回复
            Task.Run( () =>
            {
                // 记录日志
                Log.Logger.Log( "[wx: 收到消息] " + rawMsgStr );


                string reply = string.Empty;
                try
                {
                    // 解析并执行
                    reply = Wx.Message.MessageParser.ParseAndExecute( rawMsgStr );
                }
                catch ( System.NotSupportedException ex )
                {
                    Log.Logger.Log( "[wx: 处理消息异常] " + ex.Message );
                    return;
                }
                catch ( System.Exception ex )
                {
                    Log.Logger.Log( "[wx: 处理消息异常] " + ex.Message );
                    return;
                }

                if ( !string.IsNullOrEmpty( reply ) )
                {
                    // 获取token
                    var token = Wx.Utils.AccessTokenUtil.GetAccessToken( );

                    // 调用客服消息接口进行异步回复
                    var ret = Wx.Message.Service.ServiceUtil.Send( token, reply );
                    if ( ret == false )
                    {
                        Log.Logger.Log( "[wx: 客服消息发送失败]" );
                        return;
                    }
                }


            } );


            return "success";
        }



    }
}