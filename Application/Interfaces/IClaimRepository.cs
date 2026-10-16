using ClaimStatus.Api.Domain.Models;

namespace ClaimStatus.Api.Application.Interfaces;

public interface IClaimRepository
{
    Task<Claim?> GetByIdAsync(
        string claimId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Claim>> GetAllAsync(
        CancellationToken cancellationToken = default);
}