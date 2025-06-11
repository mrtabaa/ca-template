using Ca.Domain.Modules.AccessControl.Aggregates;
using Ca.Domain.Modules.AccessControl.Results;

namespace Ca.Domain.Modules.AccessControl;

public interface IAccessControlRepository
{
    public Task<AccessRoleCreationResult> CreateAppRoleAsync(AppRole appRole);
}