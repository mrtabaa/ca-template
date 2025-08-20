using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ca.Infrastructure.Persistence.EFCore.Postgres.Extensions;

// TODO create interface
internal static class ModelBuilderExtensionsPostgres
{
    internal static void UsePreventConcurrency<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class =>
        builder.Property<uint>("xmin") // shadow map to system column
            .IsRowVersion(); // concurrency token + DB-generated on add/update
}