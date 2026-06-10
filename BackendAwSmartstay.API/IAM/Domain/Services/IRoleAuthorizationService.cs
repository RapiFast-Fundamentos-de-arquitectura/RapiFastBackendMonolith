using BackendAwSmartstay.API.IAM.Domain.Model.Aggregates;

namespace BackendAwSmartstay.API.IAM.Domain.Services;

/// <summary>
/// Domain service that enforces role-hierarchy rules for user management.
/// </summary>
public interface IRoleAuthorizationService
{
    /// <summary>
    /// Returns true if the manager can perform CRUD operations on the managed user.
    /// Requires both hierarchy superiority AND scope access.
    /// </summary>
    bool CanManage(User manager, User managed);

    /// <summary>
    /// Returns true if the assigner is allowed to grant the target role to another user.
    /// </summary>
    bool CanAssignRole(User assigner, string targetRole);
}