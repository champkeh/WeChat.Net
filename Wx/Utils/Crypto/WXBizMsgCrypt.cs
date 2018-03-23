using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Wx.Utils.Crypto
{
    /// <summary>
    /// 消息体的加解密
    /// </summary>
    public class WXBizMsgCrypt
    {
        private string _token;
        private string _aeskey;
        private string _appid;


        enum WXBizMsgCryptErrorCode
        {
            /// <summary>
            /// 成功
            /// </summary>
            WXBizMsgCrypt_OK = 0,

            /// <summary>
            /// 签名验证错误
            /// </summary>
            WXBizMsgCrypt_ValidateSignature_Error = -40001,

            /// <summary>
            /// xml解析失败
            /// </summary>
            WXBizMsgCrypt_ParseXml_Error = -40002,

            /// <summary>
            /// sha加密生成签名失败
            /// </summary>
            WXBizMsgCrypt_ComputeSignature_Error = -40003,

            /// <summary>
            /// AESKey 非法
            /// </summary>
            WXBizMsgCrypt_IllegalAesKey = -40004,

            /// <summary>
            /// appid 校验错误
            /// </summary>
            WXBizMsgCrypt_ValidateAppid_Error = -40005,

            /// <summary>
            /// AES 加密失败
            /// </summary>
            WXBizMsgCrypt_EncryptAES_Error = -40006,

            /// <summary>
            /// AES 解密失败
            /// </summary>
            WXBizMsgCrypt_DecryptAES_Error = -40007,

            /// <summary>
            /// 解密后得到的buffer非法
            /// </summary>
            WXBizMsgCrypt_IllegalBuffer = -40008,

            /// <summary>
            /// base64加密异常
            /// </summary>
            WXBizMsgCrypt_EncodeBase64_Error = -40009,

            /// <summary>
            /// base64解密异常
            /// </summary>
            WXBizMsgCrypt_DecodeBase64_Error = -40010
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="token">公众平台上，开发者设置的Token</param>
        /// <param name="aeskey">公众平台上，开发者设置的EncodingAESKey</param>
        /// <param name="appid">公众帐号的appid</param>
        public WXBizMsgCrypt( string token, string aeskey, string appid )
        {
            _token = token;
            _appid = appid;
            _aeskey = aeskey;
        }


        
        /// <summary>
        /// 解密消息体
        /// </summary>
        /// <param name="msgSignature">签名串，对应URL参数的msg_signature</param>
        /// <param name="timeStamp">时间戳，对应URL参数的timestamp</param>
        /// <param name="nonce">随机串，对应URL参数的nonce</param>
        /// <param name="postData">密文，对应POST请求的数据</param>
        /// <param name="msg">解密后的原文，当return返回0时有效</param>
        /// <returns>成功0，失败返回对应的错误码</returns>
        public int DecryptMsg( string msgSignature, string timeStamp, string nonce, string postData, ref string msg )
        {
            if ( _aeskey.Length != 43 )
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
            }
            XmlDocument doc = new XmlDocument( );
            XmlNode root;
            string sEncryptMsg;
            try
            {
                doc.LoadXml( postData );
                root = doc.FirstChild;
                sEncryptMsg = root["Encrypt"].InnerText;
            }
            catch ( Exception )
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ParseXml_Error;
            }
            //verify signature
            int ret = 0;
            ret = VerifySignature( _token, timeStamp, nonce, sEncryptMsg, msgSignature );
            if ( ret != 0 )
                return ret;
            //decrypt
            string cpid = "";
            try
            {
                msg = Cryptography.AES_decrypt( sEncryptMsg, _aeskey, ref cpid );
            }
            catch ( FormatException )
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecodeBase64_Error;
            }
            catch ( Exception )
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecryptAES_Error;
            }
            if ( cpid != _appid )
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ValidateAppid_Error;
            return 0;
        }



        /// <summary>
        /// 加密消息体
        /// </summary>
        /// <param name="replyMsg">公众号待回复用户的消息，xml格式的字符串</param>
        /// <param name="timeStamp">时间戳，可以自己生成，也可以用URL参数的timestamp</param>
        /// <param name="nonce">随机串，可以自己生成，也可以用URL参数的nonce</param>
        /// <param name="encryptMsg">加密后的可以直接回复用户的密文，包括msg_signature, timestamp, nonce, encrypt的xml格式的字符串,</param>
        /// <returns>成功0，失败返回对应的错误码</returns>
        public int EncryptMsg( string replyMsg, string timeStamp, string nonce, ref string encryptMsg )
        {
            if ( _aeskey.Length != 43 )
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
            }
            string raw = "";
            try
            {
                raw = Cryptography.AES_encrypt( replyMsg, _aeskey, _appid );
            }
            catch ( Exception )
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_EncryptAES_Error;
            }
            string MsgSigature = "";
            int ret = 0;
            ret = GenarateSinature( _token, timeStamp, nonce, raw, ref MsgSigature );
            if ( 0 != ret )
                return ret;
            encryptMsg = "";

            string EncryptLabelHead = "<Encrypt><![CDATA[";
            string EncryptLabelTail = "]]></Encrypt>";
            string MsgSigLabelHead = "<MsgSignature><![CDATA[";
            string MsgSigLabelTail = "]]></MsgSignature>";
            string TimeStampLabelHead = "<TimeStamp><![CDATA[";
            string TimeStampLabelTail = "]]></TimeStamp>";
            string NonceLabelHead = "<Nonce><![CDATA[";
            string NonceLabelTail = "]]></Nonce>";
            encryptMsg = encryptMsg + "<xml>" + EncryptLabelHead + raw + EncryptLabelTail;
            encryptMsg = encryptMsg + MsgSigLabelHead + MsgSigature + MsgSigLabelTail;
            encryptMsg = encryptMsg + TimeStampLabelHead + timeStamp + TimeStampLabelTail;
            encryptMsg = encryptMsg + NonceLabelHead + nonce + NonceLabelTail;
            encryptMsg += "</xml>";
            return 0;
        }




        public class DictionarySort : System.Collections.IComparer
        {
            public int Compare( object oLeft, object oRight )
            {
                string sLeft = oLeft as string;
                string sRight = oRight as string;
                int iLeftLength = sLeft.Length;
                int iRightLength = sRight.Length;
                int index = 0;
                while ( index < iLeftLength && index < iRightLength )
                {
                    if ( sLeft[index] < sRight[index] )
                        return -1;
                    else if ( sLeft[index] > sRight[index] )
                        return 1;
                    else
                        index++;
                }
                return iLeftLength - iRightLength;

            }
        }
        //Verify Signature
        private static int VerifySignature( string sToken, string sTimeStamp, string sNonce, string sMsgEncrypt, string sSigture )
        {
            string hash = "";
            int ret = 0;
            ret = GenarateSinature( sToken, sTimeStamp, sNonce, sMsgEncrypt, ref hash );
            if ( ret != 0 )
                return ret;
            //System.Console.WriteLine(hash);
            if ( hash == sSigture )
                return 0;
            else
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ValidateSignature_Error;
            }
        }

        public static int GenarateSinature( string sToken, string sTimeStamp, string sNonce, string sMsgEncrypt, ref string sMsgSignature )
        {
            ArrayList AL = new ArrayList( );
            AL.Add( sToken );
            AL.Add( sTimeStamp );
            AL.Add( sNonce );
            AL.Add( sMsgEncrypt );
            AL.Sort( new DictionarySort( ) );
            string raw = "";
            for ( int i = 0; i < AL.Count; ++i )
            {
                raw += AL[i];
            }

            SHA1 sha;
            ASCIIEncoding enc;
            string hash = "";
            try
            {
                sha = new SHA1CryptoServiceProvider( );
                enc = new ASCIIEncoding( );
                byte[] dataToHash = enc.GetBytes( raw );
                byte[] dataHashed = sha.ComputeHash( dataToHash );
                hash = BitConverter.ToString( dataHashed ).Replace( "-", "" );
                hash = hash.ToLower( );
            }
            catch ( Exception )
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ComputeSignature_Error;
            }
            sMsgSignature = hash;
            return 0;
        }
    }
}
