namespace ClaimStatus.Api.Domain.Models;

public sealed record ClaimDocument(
    string DocumentId,
    string ClaimId,
    string FileName,
    string DocumentType,
    string Status,
    DateTime UploadedAt
);
