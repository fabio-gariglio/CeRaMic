using System.Data;

namespace CRM.Data.Common
{
	public interface IConnectionFactory
	{
		IDbConnection Create();
	}
}
