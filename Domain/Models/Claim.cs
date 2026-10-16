namespace ClaimStatus.Api.Domain.Models;

public sealed record Claim(
    string ClaimId,
    string PolicyNumber,
    string Vehicle,
    string Status,
    string SubmittedDate,
    string LastUpdated
);