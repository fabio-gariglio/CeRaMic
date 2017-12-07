namespace CRM.Security
{
	public interface IPasswordRecoveryService
	{
		void Recovery(string email);
	}
}
