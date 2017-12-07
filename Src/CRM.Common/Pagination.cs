namespace CRM
{
	public class Pagination
	{
		public int Limit { get; private set; }
		public int Skip { get; private set; }

		public Pagination() : this(0,0)
		{
			
		}

		public Pagination(int limit, int skip)
		{
			Limit = limit;
			Skip = skip;
		}
	}
}
