using CommerceHub.Application.DTOs.Auth;
using CommerceHub.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommerceHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult>Register(RegisterRequestDto registerRequestDto)
        {
            var result = await _authService.RegisterAsync(registerRequestDto);
            return Created(result,"Kayıt Başarılıdır");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var result = await _authService.LoginAsync(loginRequestDto);
            return Ok(result, "Giriş Başarılıdır");
        }

    }
}
