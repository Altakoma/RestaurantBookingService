namespace OrderService.Infrastructure.Data.ApplicationNoSqlDbContext
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
