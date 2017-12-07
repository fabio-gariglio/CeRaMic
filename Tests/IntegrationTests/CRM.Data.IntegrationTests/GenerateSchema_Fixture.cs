//using Machine.Specifications;

//namespace SI.Core.UnitTests
//{
//	public class TestEntity
//	{
//		public ObjectId Id { get; set; }
//		public string Key { get; set; }

//	}

//	[Subject("Database schema")]
//	public class When_a_database_is_empty
//	{
//		private static MongoCollection<TestEntity> _collection;
			
//			Because of = () =>
//								 {
//									 var client = new MongoClient("mongodb://crmuser:CRM1nstanc3!@crm-mongodb.cloudapp.net:27017/crm-dev");
//									 var server = client.GetServer();
//									 var database = server.GetDatabase("crm-dev");
//									 _collection = database.GetCollection<TestEntity>("test");
//								 };

//		private It should_write_an_entity = () =>
//																				{
//																					var entity = new TestEntity {Key = "one"};
//																					_collection.Insert(entity);
//																				};

//		It should_read_an_entity = () =>
//															 {
//																 var entity = _collection.FindOne(Query<TestEntity>.EQ(e => e.Key, "one"));
//															 };
//	}
//}
