using ClaimStatus.Api.Domain.Models;

namespace ClaimStatus.Api.Application.Interfaces;

public interface IClaimDocumentRepository
{
    Task<ClaimDocument> UploadAsync(
        string claimId,
        IFormFile file,
        string documentType,
        CancellationToken cancellationToken = default
    );
}
