using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AppUtils
{
    public static class JsonUtil
    {
        private static JsonSerializerSettings _jsonSettings;



        static JsonUtil()
        {
            IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverter( );
            datetimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            _jsonSettings = new JsonSerializerSettings( );

            _jsonSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            _jsonSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            _jsonSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            _jsonSettings.Converters.Add( datetimeConverter );
        }


        public static T FromJson<T>( this string json )
        {
            try
            {
                return JsonConvert.DeserializeObject<T>( json, _jsonSettings );
            }
            catch
            {
                return default( T );
            }
        }



        public static string ToJson( this object obj )
        {
            try
            {
                if ( null == obj )
                {
                    return null;
                }
                return JsonConvert.SerializeObject( obj, Formatting.None, _jsonSettings );
            }
            catch
            {
                return null;
            }
        }
    }
}
