using System.Collections.Concurrent;
using ClaimStatus.Api.Application.Interfaces;
using ClaimStatus.Api.Domain.Models;

namespace ClaimStatus.Api.Infrastructure.Repositories;

public sealed class InMemoryClaimRepository : IClaimRepository
{
    private readonly ConcurrentDictionary<string, Claim> _claims;

    public InMemoryClaimRepository()
    {
        _claims = new ConcurrentDictionary<string, Claim>(
            new Dictionary<string, Claim>
            {
                ["CLM1001"] = new Claim(
                    "CLM1001",
                    "POL98765",
                    "Honda City",
                    "UNDER_REVIEW",
                    "2026-09-20",
                    "2026-09-28"
                ),

                ["CLM1002"] = new Claim(
                    "CLM1002",
                    "POL98766",
                    "Hyundai Creta",
                    "APPROVED",
                    "2026-09-18",
                    "2026-09-27"
                ),

                ["CLM1003"] = new Claim(
                    "CLM1003",
                    "POL98767",
                    "Tata Nexon",
                    "DOCUMENTS_REQUIRED",
                    "2026-09-22",
                    "2026-09-28"
                ),

                ["CLM1004"] = new Claim(
                    "CLM1004",
                    "POL98768",
                    "Maruti Swift",
                    "REJECTED",
                    "2026-09-15",
                    "2026-09-25"
                )
            });
    }

    public Task<Claim?> GetByIdAsync(
        string claimId,
        CancellationToken cancellationToken = default)
    {
        _claims.TryGetValue(
            claimId.ToUpperInvariant(),
            out var claim);

        return Task.FromResult(claim);
    }

    public Task<IReadOnlyCollection<Claim>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<Claim> claims =
            _claims.Values.ToList();

        return Task.FromResult(claims);
    }
}