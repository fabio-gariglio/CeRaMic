using System.Net;
using System.Net.Mail;
using CRM.Configuration;

namespace CRM.Mail
{
	public class MailService : IMailService, ISingleton
	{
		private readonly IConfigurationProvider _configurationProvider;

		public MailService(IConfigurationProvider configurationProvider)
		{
			_configurationProvider = configurationProvider;
		}

		public void SendMail(MailContract mail)
		{
			var message = CreateMessage(mail);

			var userName = _configurationProvider.GetApplicationSetting<string>("SmtpUsername");
			var password = _configurationProvider.GetApplicationSetting<string>("SmtpPassword");
			var credential = new NetworkCredential(userName, password);

			var client = new SmtpClient
			             {
				             DeliveryMethod = SmtpDeliveryMethod.Network,
				             UseDefaultCredentials = false,
										 Credentials = credential,
										 Port = _configurationProvider.GetApplicationSetting("SmtpPort", 25),
										 Host = _configurationProvider.GetApplicationSetting<string>("SmtpHost")
			             };

			client.Send(message);
		}

		private static MailMessage CreateMessage(MailContract mail)
		{
			var message = new MailMessage(mail.Sender, mail.Recipient)
			              {
				              Subject = mail.Subject,
				              Body = mail.Body, 
											IsBodyHtml = true
			              };

			return message;
		}
	}
}
