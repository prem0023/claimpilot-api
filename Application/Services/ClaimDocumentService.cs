using ClaimStatus.Api.Application.Interfaces;
using ClaimStatus.Api.Domain.Models;

namespace ClaimStatus.Api.Application.Services;

public sealed class ClaimDocumentService : IClaimDocumentService
{
    private readonly IClaimDocumentRepository _repository;

    public ClaimDocumentService(IClaimDocumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClaimDocument> UploadAsync(
        string claimId,
        IFormFile file,
        string documentType,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(claimId))
            throw new ArgumentException("Claim ID is required.", nameof(claimId));

        if (file is null || file.Length == 0)
            throw new ArgumentException("A valid document is required.", nameof(file));

        if (string.IsNullOrWhiteSpace(documentType))
            throw new ArgumentException("Document type is required.", nameof(documentType));

        var normalizedClaimId = claimId.Trim().ToUpperInvariant();

        var normalizedDocumentType = documentType.Trim().ToUpperInvariant();

        return await _repository.UploadAsync(
            normalizedClaimId,
            file,
            normalizedDocumentType,
            cancellationToken
        );
    }
}
