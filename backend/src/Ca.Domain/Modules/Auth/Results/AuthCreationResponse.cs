using System.Runtime.InteropServices;
using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Domain.Modules.Auth.Enums;

namespace Ca.Domain.Modules.Auth.Results;

public record AuthCreationResponse(
    AppUser? AppUser,
    [Optional] AuthCreationResult AuthCreationResult
);