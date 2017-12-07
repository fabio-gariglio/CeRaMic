namespace CRM.Mail
{
	public class MailContract
	{
		public string Sender { get; set; }
		public string Recipient{ get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
	}
}