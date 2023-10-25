namespace OrderService.Infrastructure.Data.ApplicationNoSqlDbContext
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
    }
}
