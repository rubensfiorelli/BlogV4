using BlogV4.Application.Extensions;
using BlogV4.Application.Notifications;
using BlogV4.Application.Repositories;
using BlogV4.Domain.DTOs.Input;
using BlogV4.Domain.DTOs.Output;
using BlogV4.Domain.Entities;
using BlogV4.IoC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace BlogV4.Controllers
{
    [Route("v4")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IUserService _userService;
        public AccountsController(TokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpGet("accounts/{userId}")]
        public async Task<IActionResult> GetById(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _userService.GetUserId(userId, cancellationToken);

                if (existing is null)
                    return NotFound(new Notification<UserOutputDto>("User não localizado"));

                return Ok(existing);

            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new Notification<CreateUserDto>("Falha interna no servidor"));
            }

        }

        [HttpPost("accounts/")]
        public async Task<IActionResult> Post(CreateUserDto userDto)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new Notification<string>(ModelState.GetErrors()));

                var user = await _userService.Add(userDto);

                return Ok(new Notification<dynamic>(userDto.Name, userDto.Email, userDto.Password));

            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new Notification<CreateUserDto>("Usuário já cadastrado"));
            }
            catch
            {
                return StatusCode(500, new Notification<CreateUserDto>("Erro interno no servidor"));
            }

        }

        [HttpPost("accounts/login")]
        public async Task<IActionResult> Login(CreateLoginDto model, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                    return BadRequest(new Notification<string>(ModelState.GetErrors()));

            var existingDto = _userService.GetUserEmail(model.Email, cancellationToken);

            if (existingDto is null)
                    return StatusCode(401, new Notification<string>("Usuário / Senha inválidos"));

           
            //if (!PasswordHasher.Verify(existingDto.PasswordHash, model.Password))
            //        return StatusCode(401, new Notification<string>("Usuário / Senha inválidos"));

            //try
            //{
            //    var token = _tokenService.GenerateToken(existingEntity);
            //        return Ok(new Notification<string>(token, null));
            //}
            //catch
            //{
            //    return StatusCode(500, new Notification<string>("Falha interna no servidor"));
            //}
           
            return Ok(existingDto);
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult GetUser() => Ok(User.GetUserName());

        [Authorize]
        [HttpGet("userId")]
        public IActionResult GetId() => Ok(User.GetUserId());


    }
}
