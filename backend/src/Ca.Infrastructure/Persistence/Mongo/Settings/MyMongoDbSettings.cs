namespace Ca.Infrastructure.Persistence.Mongo.Settings;

public class MyMongoDbSettings : IMyMongoDbSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
}