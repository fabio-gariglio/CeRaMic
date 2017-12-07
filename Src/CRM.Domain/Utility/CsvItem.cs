using System.Reflection;
using CsvHelper;

namespace CRM.Domain.Utility
{
	internal class CsvItem
	{
		private static readonly PropertyInfo[] Properties;

		static CsvItem()
		{
			Properties = typeof(CsvItem).GetProperties(BindingFlags.Public | BindingFlags.Instance);
		}

		public string Nome { get; set; }
		public string Cognome { get; set; }
		public string Cliente { get; set; }
		public string Posizione { get; set; }
		public string Segretaria { get; set; }
		public string Owner { get; set; }
		public string Partner { get; set; }
		public string Note { get; set; }
		public string Stato { get; set; }
		public string TipoRecapito { get; set; }
		public string Recapito { get; set; }

		public static CsvItem FromCsvReader(CsvReader reader)
		{
			var result = new CsvItem();

			foreach (var property in Properties)
			{
				var value = reader.GetField<string>(property.Name);

				if(string.IsNullOrWhiteSpace(value)) continue;

				property.SetValue(result, value != null ? value.Trim() : null);
			}

			return result;
		}

	}
}