using ClaimStatus.Api.Application.Interfaces;
using ClaimStatus.Api.Domain.Models;

namespace ClaimStatus.Api.Application.Services;

public sealed class ClaimService : IClaimService
{
    private readonly IClaimRepository _claimRepository;

    public ClaimService(IClaimRepository claimRepository)
    {
        _claimRepository = claimRepository;
    }

    public async Task<Claim?> GetClaimAsync(
        string claimId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(claimId))
        {
            throw new ArgumentException(
                "Claim ID is required.",
                nameof(claimId));
        }

        var normalizedClaimId =
            claimId.Trim().ToUpperInvariant();

        return await _claimRepository.GetByIdAsync(
            normalizedClaimId,
            cancellationToken);
    }

    public async Task<IReadOnlyCollection<Claim>> GetAllClaimsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _claimRepository.GetAllAsync(
            cancellationToken);
    }
}