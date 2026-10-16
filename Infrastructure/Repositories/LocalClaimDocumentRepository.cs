using ClaimStatus.Api.Application.Interfaces;
using ClaimStatus.Api.Domain.Models;

namespace ClaimStatus.Api.Infrastructure.Repositories;

public sealed class LocalClaimDocumentRepository : IClaimDocumentRepository
{
    private readonly IWebHostEnvironment _environment;

    public LocalClaimDocumentRepository(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<ClaimDocument> UploadAsync(
        string claimId,
        IFormFile file,
        string documentType,
        CancellationToken cancellationToken = default
    )
    {
        var documentsPath = Path.Combine(
            _environment.ContentRootPath,
            "Storage",
            "Claims",
            claimId
        );

        Directory.CreateDirectory(documentsPath);

        var documentId = $"DOC-{Guid.NewGuid():N}".ToUpperInvariant();

        var extension = Path.GetExtension(file.FileName);

        var storedFileName = $"{documentId}{extension}";

        var filePath = Path.Combine(documentsPath, storedFileName);

        await using var stream = new FileStream(
            filePath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None
        );

        await file.CopyToAsync(stream, cancellationToken);

        return new ClaimDocument(
            DocumentId: documentId,
            ClaimId: claimId,
            FileName: file.FileName,
            DocumentType: documentType,
            Status: "UPLOADED",
            UploadedAt: DateTime.UtcNow
        );
    }
}
