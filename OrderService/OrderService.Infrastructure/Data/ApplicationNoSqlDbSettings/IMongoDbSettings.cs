namespace OrderService.Infrastructure.Data.ApplicationNoSqlDbSettings
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
