using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ca.Infrastructure.Persistence.EFCore.Common;

public interface ICustomModelBuilder
{
    void UsePreventConcurrency<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : class;
}