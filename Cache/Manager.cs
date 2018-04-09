using StackExchange.Redis;

namespace Cache
{
    class Manager1
    {
        private static ConnectionMultiplexer _redis;
        private static object _locker = new object();

        public static ConnectionMultiplexer Manager
        {
            get
            {
                if ( _redis == null )
                {
                    lock ( _locker )
                    {
                        if ( _redis != null )
                            return _redis;

                        _redis = GetManager( );
                        return _redis;
                    }
                }

                return _redis;
            }
        }

        private static ConnectionMultiplexer GetManager( string connectionString = null )
        {
            

            return ConnectionMultiplexer.Connect( connectionString );
        }
    }
}
