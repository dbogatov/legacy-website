using System;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Newtonsoft.Json;

public static class SessionExtensions
{
	public static void SetObjectAsJson(this ISession session, string key, object value)
	{
		session.SetString(key, JsonConvert.SerializeObject(value));
	}

	public static T GetObjectFromJson<T>(this ISession session, string key)
	{
		var value = session.GetString(key);

		return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
	}

	public static bool? GetBoolean(this ISession session, string key)
	{
		var data = session.Get(key);
		if (data == null)
		{
			return null;
		}
		return BitConverter.ToBoolean(data, 0);
	}

	public static void SetBoolean(this ISession session, string key, bool value)
	{
		session.Set(key, BitConverter.GetBytes(value));
	}
}