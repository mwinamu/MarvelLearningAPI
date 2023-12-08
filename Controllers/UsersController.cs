using Marten;
using MarvelLearningAPI.Domain;
using MarvelLearningAPI.Event;
using MarvelLearningAPI.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MarvelLearningAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository) => _userRepository = userRepository;

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest create)
    {
        await _userRepository.CreateUser(create);
        return Ok();
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers([FromQuery] bool internalOnly, CancellationToken ct)
    {
        var users = await _userRepository.GetUsers(internalOnly, ct);
        return Ok(users);
    }

    [HttpGet("user/{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id, CancellationToken ct)
    {
        var user = await _userRepository.GetUser(id, ct);
        if (user == null)
            return NotFound();

        return Ok(user);
    }
}

