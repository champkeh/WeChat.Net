using System;

namespace Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 获取时间戳，单位秒
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static string GetTimeStamp( this DateTime now )
        {
            TimeSpan ts = now.ToUniversalTime( ) - new DateTime( 1970, 1, 1 );
            return Convert.ToInt64( ts.TotalSeconds ).ToString( );
        }


        /// <summary>
        /// 获取时间戳，单位毫秒
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static string GetTimeStamp2( this DateTime now )
        {
            TimeSpan ts = now.ToUniversalTime( ) - new DateTime( 1970, 1, 1 );
            return Convert.ToInt64( ts.TotalMilliseconds ).ToString( );
        }

    }
}
