using NLog;

namespace Log
{
    public class Logger
    {
        public static void Log( string content )
        {
            LogManager.GetCurrentClassLogger( ).Info( content );
        }
    }
}
