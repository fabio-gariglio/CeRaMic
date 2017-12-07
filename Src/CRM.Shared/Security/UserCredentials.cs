namespace CRM.Security
{
	public sealed class UserCredentials
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
