using System;
using System.Configuration;

namespace AppUtils
{
    public sealed class ConfigUtil
    {
        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString( string key )
        {
            try
            {
                var appValue = ConfigurationManager.AppSettings[key];
                if ( appValue != null )
                {
                    return appValue.ToString( );
                }
                else
                {
                    return null;
                }
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[log: 警告] 获取config配置异常: " + ex.InnerException + "|" + ex.Message );
            }
            return null;
        }



        /// <summary>
        /// 得到AppSettings中的配置Bool信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetConfigBool( string key )
        {
            bool result = false;
            string cfgVal = GetConfigString( key );
            if ( null != cfgVal && string.Empty != cfgVal )
            {
                try
                {
                    result = bool.Parse( cfgVal );
                }
                catch ( FormatException )
                {
                    // Ignore format exceptions.
                }
            }
            return result;
        }



        /// <summary>
        /// 得到AppSettings中的配置Decimal信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetConfigDecimal( string key )
        {
            decimal result = 0;
            string cfgVal = GetConfigString( key );
            if ( null != cfgVal && string.Empty != cfgVal )
            {
                try
                {
                    result = decimal.Parse( cfgVal );
                }
                catch ( FormatException )
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }



        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetConfigInt( string key )
        {
            int result = 0;
            string cfgVal = GetConfigString( key );
            if ( null != cfgVal && string.Empty != cfgVal )
            {
                try
                {
                    result = int.Parse( cfgVal );
                }
                catch ( FormatException )
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
    }
}
