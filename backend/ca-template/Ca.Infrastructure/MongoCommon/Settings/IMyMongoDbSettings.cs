namespace Ca.Infrastructure.MongoCommon.Settings;

public interface IMyMongoDbSettings
{
    string? ConnectionString { get; }
    string? DatabaseName { get; }
}