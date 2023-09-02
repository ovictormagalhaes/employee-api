using Auth.API.Application.Inputs;
using Auth.API.Application.Outputs;
using Auth.API.Application.Services;
using Auth.API.Infrastructure.Repositories;
using Auth.API.Infrastructure.Securities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IValidator<CreateUserInput> _createUserInputValidator;
    private readonly IValidator<LoginInput> _loginInputValidator;
    public AuthController(
        IAuthService userService,
        IUserRepository userRepository,
        ITokenGenerator tokenGenerator,
        IValidator<CreateUserInput> createUserInputValidable,
        IValidator<LoginInput> loginInputValidator)
    {
        _authService = userService;
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
        _createUserInputValidator = createUserInputValidable;
        _loginInputValidator = loginInputValidator;
    }

    [Authorize]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserInput input)
    {
        var validate = _createUserInputValidator.Validate(input);

        if (!validate.IsValid)
            return BadRequest("Invalid information");

        var currentUser = await _userRepository.FirstOrDefaultByEmailAsync(input.Email!);
        if (currentUser is not null)
            return Conflict("E-mail already exists");

        var user = await _authService.RegisterAsync(input);

        var output = new CreateUserOutput(user.Id, user.Name, user.Email, user.CreatedAt);

        return Created(user.Id.ToString(), output);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginInput input)
    {
        var validate = _loginInputValidator.Validate(input);

        if (!validate.IsValid)
            return BadRequest("Invalid information");

        var currentUser = await _authService.LoginAsync(input);
        if (currentUser is null)
            return Unauthorized("Invalid e-mail or password");

        var token = _tokenGenerator.Generate(currentUser.Id.ToString(), currentUser.Name, currentUser.Email);
        var output = new LoginOutput(
            currentUser.Id, currentUser.Name,
            currentUser.Email, token, currentUser.CreatedAt);

        return Ok(output);
    }
}