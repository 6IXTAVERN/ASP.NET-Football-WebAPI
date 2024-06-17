using Microsoft.AspNetCore.Identity;
using WebAPI.Domain.Models;

namespace WebAPI.BusinessLogicLayer.Services.TokenService ;


public interface ITokenService
{
    public Task<string> GenerateJwtToken(IdentityUser user, TimeSpan expiration);
}