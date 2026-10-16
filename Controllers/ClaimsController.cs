using ClaimStatus.Api.Application.Interfaces;
using ClaimStatus.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClaimStatus.Api.Controllers;

[ApiController]
[Route("api/claims")]
public sealed class ClaimsController : ControllerBase
{
    private readonly IClaimService _claimService;
    private readonly IClaimDocumentService _claimDocumentService;
    private readonly ILogger<ClaimsController> _logger;

    public ClaimsController(
        IClaimService claimService,
        IClaimDocumentService claimDocumentService,
        ILogger<ClaimsController> logger
    )
    {
        _claimService = claimService;
        _claimDocumentService = claimDocumentService;
        _logger = logger;
    }

    [HttpGet("{claimId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetClaim(string claimId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(claimId))
        {
            return BadRequest(
                new ProblemDetails
                {
                    Title = "Invalid claim ID",
                    Detail = "Claim ID is required.",
                    Status = StatusCodes.Status400BadRequest,
                }
            );
        }

        var claim = await _claimService.GetClaimAsync(claimId, cancellationToken);

        if (claim is null)
        {
            return NotFound(
                new ProblemDetails
                {
                    Title = "Claim not found",
                    Detail = $"Claim {claimId} was not found.",
                    Status = StatusCodes.Status404NotFound,
                }
            );
        }

        return Ok(claim);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllClaims(CancellationToken cancellationToken)
    {
        var claims = await _claimService.GetAllClaimsAsync(cancellationToken);

        return Ok(claims);
    }

    [HttpPost("{claimId}/documents")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadDocument(
        string claimId,
        [FromForm] UploadClaimDocumentRequest request,
        CancellationToken cancellationToken
    )
    {
        if (string.IsNullOrWhiteSpace(claimId))
        {
            return BadRequest(
                new ProblemDetails
                {
                    Title = "Invalid claim ID",
                    Detail = "Claim ID is required.",
                    Status = StatusCodes.Status400BadRequest,
                }
            );
        }

        if (request.File is null || request.File.Length == 0)
        {
            return BadRequest(
                new ProblemDetails
                {
                    Title = "Invalid document",
                    Detail = "A non-empty document file is required.",
                    Status = StatusCodes.Status400BadRequest,
                }
            );
        }

        if (string.IsNullOrWhiteSpace(request.DocumentType))
        {
            return BadRequest(
                new ProblemDetails
                {
                    Title = "Invalid document type",
                    Detail = "Document type is required.",
                    Status = StatusCodes.Status400BadRequest,
                }
            );
        }

        var document = await _claimDocumentService.UploadAsync(
            claimId,
            request.File,
            request.DocumentType,
            cancellationToken
        );

        return Ok(document);
    }
}
