using KitM4.Blog.Api.Utilities;
using KitM4.Blog.Core.Services.Interfaces;
using KitM4.Blog.Domain.Common;
using KitM4.Blog.Domain.Exceptions;
using KitM4.Blog.Domain.Communication.Requests;
using KitM4.Blog.Domain.Communication.Responses;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KitM4.Blog.Api.Controllers;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController(IAuthService service) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] AuthRequests.Register request, CancellationToken ct)
    {
        try
        {
            UserResponse response = await service.RegisterAsync(request, ct);

            return Ok(response);
        }
        catch (AlreadyExistException alreadyExists)
        {
            return Conflict(alreadyExists.Message);
        }
        catch (InvalidRequestDataException invalidRequestData)
        {
            return BadRequest(invalidRequestData.Message);
        }
        catch (Exception)
        {
            return BadRequest(ErrorMessages.BaseError);
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] AuthRequests.Login request, CancellationToken ct)
    {
        try
        {
            UserResponse response = await service.LoginAsync(request, ct);

            return Ok(response);
        }
        catch (IncorrectCredentialsException incorrectCredentials)
        {
            return Forbid(incorrectCredentials.Message);
        }
        catch (NotFoundException notFound)
        {
            return NotFound(notFound.Message);
        }
        catch (InvalidRequestDataException invalidRequestData)
        {
            return BadRequest(invalidRequestData.Message);
        }
        catch (Exception)
        {
            return BadRequest(ErrorMessages.BaseError);
        }
    }

    [HttpDelete("delete")]
    [Authorize(Policy = AuthPolicy.All)]
    public async Task<IActionResult> DeleteAsync(CancellationToken ct)
    {
        try
        {
            await service.DeleteAsync(User, ct);

            return NoContent();
        }
        catch (NotFoundException notFound)
        {
            return NotFound(notFound.Message);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (Exception)
        {
            return BadRequest(ErrorMessages.BaseError);
        }
    }

    #region Admin Endpoints

    [HttpPost("change-role")]
    [Authorize(Policy = AuthPolicy.Admin)]
    public async Task<IActionResult> ChangeRoleAsync([FromBody] AuthRequests.ChangeRole request, CancellationToken ct)
    {
        try
        {
            await service.ChangeRoleAsync(request, ct);

            return NoContent();
        }
        catch (NotFoundException notFound)
        {
            return NotFound(notFound.Message);
        }
        catch (InvalidRequestDataException invalidRequestData)
        {
            return BadRequest(invalidRequestData.Message);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (Exception)
        {
            return BadRequest(ErrorMessages.BaseError);
        }
    }

    #endregion
}