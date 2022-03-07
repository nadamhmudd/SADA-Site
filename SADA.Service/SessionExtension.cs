using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SADA.Service;

//Extension Methods for Session
public static class SessionExtension
{
    public static void SetObject(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T GetObject<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }
    public static void IncrementValue(this ISession session, string key, int inc)
    {
        int? value = session.GetInt32(key);
        if(value != null)
        {
            session.SetInt32(key, (int)value + inc);
        }

    }
    public static void DecrementValue(this ISession session, string key, int dec)
    {
        int? value = session.GetInt32(key);
        if (value != null)
        {
            session.SetInt32(key, (int)value - dec);
        }
    }
}
