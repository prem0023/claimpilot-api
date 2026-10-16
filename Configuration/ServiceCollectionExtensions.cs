using ClaimStatus.Api.Application.Interfaces;
using ClaimStatus.Api.Application.Services;
using ClaimStatus.Api.Infrastructure.Repositories;

namespace ClaimStatus.Api.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClaimStatusServices(this IServiceCollection services)
    {
        services.AddScoped<IClaimService, ClaimService>();

        services.AddScoped<IClaimDocumentService, ClaimDocumentService>();

        services.AddSingleton<IClaimRepository, InMemoryClaimRepository>();

        services.AddSingleton<IClaimDocumentRepository, LocalClaimDocumentRepository>();

        return services;
    }
}
