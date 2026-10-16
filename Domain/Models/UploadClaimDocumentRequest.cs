namespace ClaimStatus.Api.Domain.Models;

public sealed class UploadClaimDocumentRequest
{
    public IFormFile? File { get; set; }

    public string DocumentType { get; set; } = string.Empty;
}
