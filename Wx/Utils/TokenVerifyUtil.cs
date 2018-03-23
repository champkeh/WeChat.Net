using System.Collections.Generic;
using System.Linq;

namespace Wx.Utils
{
    /// <summary>
    /// 接入开发者模式时的Token验证
    /// </summary>
    public class TokenVerifyUtil
    {
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static bool VerifySign( string signature, string timestamp, string nonce )
        {
            // 获取公众号配置的token
            var token = Wx.Config.Token;

            SortedSet<string> array = new SortedSet<string>( );
            array.Add( token );
            array.Add( timestamp );
            array.Add( nonce );

            var str = array.Aggregate( ( s1, s2 ) => s1 + s2 );

            // 签名验证
            var sign = AppUtils.Sha1.SHA1WithUtf8( str ).ToLower( );

            return sign == signature;
        }
    }
}
