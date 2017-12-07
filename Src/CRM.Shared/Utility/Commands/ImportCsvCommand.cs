using CRM.EventSourcing;

namespace CRM.Utility
{
	public class ImportCsvCommand : DomainCommand
	{
		public string CsvContent { get; set; }

		public ImportCsvCommand(string csvContent)
		{
			CsvContent = csvContent;
		}
	}
}
