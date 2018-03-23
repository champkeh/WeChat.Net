using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace AppUtils
{
    public class HttpUtil
    {
        private CookieContainer _cookie = new CookieContainer();
        private WebProxy _proxy;

        private int _delayTime = 0; // 每次请求之前延迟时间，默认不延迟
        private int _timeout = 120000; // The default is 120000 milliseconds (120 seconds).
        private int _tryTimes = 3; // 失败重试次数，默认重试3次


        public string Get( string url )
        {
            int failedTimes = _tryTimes;
            while ( failedTimes-- > 0 )
            {
                try
                {
                    if ( _delayTime > 0 )
                    {
                        Thread.Sleep( _delayTime * 1000 );
                    }
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create( new Uri( url ) );
                    req.CookieContainer = _cookie;


                    req.Method = "GET";
                    req.Timeout = _timeout;
                    req.KeepAlive = false;
                    req.ProtocolVersion = HttpVersion.Version11;
                    if ( null != _proxy && null != _proxy.Credentials )
                    {
                        req.UseDefaultCredentials = true;
                    }
                    req.Proxy = _proxy;

                    //接收返回字串
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse( );
                    StreamReader sr = new StreamReader( res.GetResponseStream( ), Encoding.UTF8 );
                    string stHTML = sr.ReadToEnd( );

                    req.Abort( );
                    res.Close( );
                    sr.Close( );

                    return stHTML;
                }
                catch ( Exception ex )
                {
                    return "[Request ERROR]" + ex.Message;
                }
            }

            return null;
        }






        public string PostJson( string url, string content )
        {
            try
            {
                if ( _delayTime > 0 )
                {
                    Thread.Sleep( _delayTime * 1000 );
                }
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create( new Uri( url ) );
                req.CookieContainer = _cookie;

                byte[] buff = Encoding.UTF8.GetBytes( content );
                req.Method = "POST";
                req.Timeout = _timeout;
                req.ContentType = "application/json";
                req.ContentLength = buff.Length;

                if ( null != _proxy && null != _proxy.Credentials )
                {
                    req.UseDefaultCredentials = true;
                }
                req.Proxy = _proxy;

                Stream reqStream = req.GetRequestStream( );
                reqStream.Write( buff, 0, buff.Length );
                reqStream.Close( );

                //接收返回字串
                HttpWebResponse res = (HttpWebResponse)req.GetResponse( );
                StreamReader sr = new StreamReader( res.GetResponseStream( ), Encoding.UTF8 );
                return sr.ReadToEnd( );
            }
            catch ( Exception ex )
            {
                return "[Request ERROR]" + ex.Message;
            }
        }





    }
}
