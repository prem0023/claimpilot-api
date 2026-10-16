using ClaimStatus.Api.Domain.Models;

namespace ClaimStatus.Api.Application.Interfaces;

public interface IClaimService
{
    Task<Claim?> GetClaimAsync(
        string claimId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Claim>> GetAllClaimsAsync(
        CancellationToken cancellationToken = default);
}