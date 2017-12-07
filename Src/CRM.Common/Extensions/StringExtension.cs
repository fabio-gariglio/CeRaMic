using System;
using System.Globalization;
using System.Text;

namespace CRM.Extensions
{
	public static class StringExtension
	{
		private static readonly Random Random = new Random((int) DateTime.Now.Ticks);
		public static string Randomize(int size)
		{
			var builder = new StringBuilder();

			for (var i = 0; i < size; i++)
			{
				var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65)));
				builder.Append(ch);
			}

			return builder.ToString();
		}

		public static string BuildFullName(string firstName, string lastName)
		{
			if (!string.IsNullOrWhiteSpace(lastName))
			{
				var result = ToCamelCase(lastName);

				if (!string.IsNullOrWhiteSpace(firstName))
				{
					result += " " + ToCamelCase(firstName);
				}

				return result;
			}

			if (!string.IsNullOrWhiteSpace(firstName))
			{
				return ToCamelCase(firstName);
			}

			return null;
		}

		public static string ToCamelCase(string value)
		{
			if (string.IsNullOrWhiteSpace(value)) return null;

			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.Trim());
		}
	}
}
