using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BusinessLogicLayer.Services.TokenService;
using WebAPI.DTO.AccountDTO;

namespace WebAPI.Controllers ;


[ApiController]
[Route("user")]
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _config;

    public AccountController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        ITokenService tokenService, IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _config = config;
    }

    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        var createResult = await _userManager.CreateAsync(user, model.Password!);

        if (!createResult.Succeeded)
        {
            return new BadRequestObjectResult(createResult.Errors);
        }

        var assignRoleResult = await _userManager.AddToRoleAsync(user, "User");

        if (!assignRoleResult.Succeeded)
        {
            return new BadRequestObjectResult(assignRoleResult.Errors);
        }

        return Ok();
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResultDto>> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _signInManager.PasswordSignInAsync(
            userName: model.Email!,
            password: model.Password!,
            isPersistent: model.RememberMe,
            lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return BadRequest("Incorrect email or password");
        }

        // Generate JWT
        var user = await _userManager.FindByNameAsync(model.Email!);
        var token = await _tokenService.GenerateJwtToken(user!, TimeSpan.FromMinutes(30));

        if (model.RememberMe)
        {
            Response.Cookies.Append(
                key: _config["Jwt:CookieName"]!,
                value: token,
                new CookieOptions
                {
                    HttpOnly = true,
                    IsEssential = true,
                    MaxAge = TimeSpan.FromMinutes(30),
                    SameSite = SameSiteMode.None,
                    Secure = true,
                });
        }

        return new OkObjectResult(
            new AuthResultDto(
                email: user!.Email!, 
                authToken: token, 
                roles: await _userManager.GetRolesAsync(user)
                )
            );
    }

    [HttpPost]
    [Route("logout")]
    [AllowAnonymous]
    public IActionResult Logout()
    {
        Response.Cookies.Append(
            key: _config["Jwt:CookieName"]!,
            value: string.Empty,
            new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                MaxAge = TimeSpan.Zero,
                SameSite = SameSiteMode.None,
                Secure = true,
            });

        return Ok();
    }
}