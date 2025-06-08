namespace Ca.Shared.Configurations.Mongo.Settings;

public interface IMyMongoDbSettings
{
    string? ConnectionString { get; }
    string? DatabaseName { get; }
}