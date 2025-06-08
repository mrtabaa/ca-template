namespace Ca.Shared.Configurations.Mongo.Settings;

public class MyMongoDbSettings : IMyMongoDbSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
}