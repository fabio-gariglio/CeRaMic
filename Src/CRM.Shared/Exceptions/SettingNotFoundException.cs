using System;

namespace CRM.Exceptions
{
	public class SettingNotFoundException : CrmException
	{
		public SettingNotFoundException()
		{
		}

		public SettingNotFoundException(string message)
			: base(message)
		{
		}

		public SettingNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}