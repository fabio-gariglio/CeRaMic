using System.Security.Cryptography;
using System.Text;

namespace CRM.Common
{
	public class EncriptionService : IEncriptionService, ISingleton
	{
		private readonly MD5 _hashFactory;

		public EncriptionService()
		{
			_hashFactory = MD5.Create();
		}

		public string CalculateHash(string value)
		{
			var inputBytes = Encoding.ASCII.GetBytes(value);
			var hash = _hashFactory.ComputeHash(inputBytes);

			// step 2, convert byte array to hex string
			var sb = new StringBuilder();

			foreach (byte t in hash)
			{
				sb.Append(t.ToString("X2"));
			}
			return sb.ToString().ToLower();
		}
	}
}
