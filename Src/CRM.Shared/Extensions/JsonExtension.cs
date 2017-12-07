using Newtonsoft.Json;

namespace CRM.Extensions
{
	public static class JsonExtension
	{
		public static T To<T>(this string obj)
			where T : class
		{
			return !string.IsNullOrEmpty(obj)
				? JsonConvert.DeserializeObject<T>(obj)
				: null;
		}

		public static string ToJson(this object obj)
		{
			return null != obj
				? JsonConvert.SerializeObject(obj)
				: null;
		}
	}
}
