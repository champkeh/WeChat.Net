using System;
using System.Security.Cryptography;
using System.Text;

namespace AppUtils
{
    public class Sha1
    {
        private static string SHA1( string content, Encoding encode )
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider( );
                byte[] bytes_in = encode.GetBytes( content );
                byte[] bytes_out = sha1.ComputeHash( bytes_in );
                sha1.Dispose( );
                return BitConverter.ToString( bytes_out ).Replace( "-", "" );
            }
            catch ( Exception ex )
            {
                throw new Exception( "SHA1加密出错：" + ex.Message );
            }
        }

        public static string SHA1WithUtf8( string content )
        {
            return SHA1( content, UTF8Encoding.UTF8 );
        }
    }
}
