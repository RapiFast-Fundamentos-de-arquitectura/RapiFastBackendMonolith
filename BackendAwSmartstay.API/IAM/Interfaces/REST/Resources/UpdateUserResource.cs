namespace BackendAwSmartstay.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// Resource definition for updating an existing user. All fields are optional.
/// </summary>
public record UpdateUserResource(
    string? NewUsername,
    string? NewPassword,
    int? NewHotelId,
    int? NewChainId);