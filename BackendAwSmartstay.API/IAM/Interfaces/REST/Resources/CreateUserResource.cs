using System.ComponentModel.DataAnnotations;

namespace BackendAwSmartstay.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// Resource definition for creating a new user via management endpoints.
/// </summary>
public record CreateUserResource(
    [Required] string Username,
    [Required] string Password,
    [Required] string Role,
    int? HotelId,
    int? ChainId);