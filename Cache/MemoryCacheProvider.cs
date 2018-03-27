using Cache.Models;
using System.Collections.Generic;

namespace Cache
{
    public class MemoryCacheProvider
    {
        static Dictionary<string, WebAuthorizeUserModel> _cache = new Dictionary<string, WebAuthorizeUserModel>();



        public static void Append( string key, WebAuthorizeUserModel user )
        {
            _cache.Add( key, user );
        }


        public static WebAuthorizeUserModel Get( string key )
        {
            return _cache[key];
        }

        public static bool Exist( string key )
        {
            return _cache.ContainsKey( key );
        }
    }
}
