using Ca.Infrastructure.Persistence.EFCore.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ca.Infrastructure.Persistence.EFCore.Postgres.Extensions;

// TODO create interface
internal class CustomModelBuilderPostgres : ICustomModelBuilder
{
    public void UsePreventConcurrency<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : class =>
        builder.Property<uint>("xmin") // shadow map to system column
            .IsRowVersion(); // concurrency token + DB-generated on add/update
}
