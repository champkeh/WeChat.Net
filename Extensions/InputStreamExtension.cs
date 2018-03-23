using System.IO;
using System.Text;

namespace Extensions
{
    public static class InputStreamExtension
    {
        /// <summary>
        /// 从流中读取出所有的字符串
        /// </summary>
        /// <param name="input">流</param>
        /// <returns></returns>
        public static string ReadAllString( this Stream input )
        {
            byte[] buffer = new byte[input.Length];
            input.Read( buffer, 0, (int)input.Length );

            return Encoding.UTF8.GetString( buffer );
        }
    }
}
