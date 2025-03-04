using KitM4.Blog.Api.Utilities;
using KitM4.Blog.Core.Services.Interfaces;
using KitM4.Blog.Domain.Common;
using KitM4.Blog.Domain.Exceptions;
using KitM4.Blog.Domain.Communication.Requests;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KitM4.Blog.Api.Controllers;

[ApiController]
[Route("/api/v1/blog")]
public class BlogController(IBlogService service) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("author")]
    public async Task<IActionResult> GetAuthorAsync(CancellationToken ct)
    {
        try
        {
            return Ok(await service.GetAuthorAsync(ct));
        }
        catch (NotFoundException notFound)
        {
            return NotFound(notFound.Message);
        }
        catch (Exception)
        {
            return BadRequest(ErrorMessages.BaseError);
        }
    }

    [AllowAnonymous]
    [HttpGet("articles")]
    public async Task<IActionResult> GetAllArticlesAsync(CancellationToken ct)
    {
        try
        {
            return Ok(await service.GetAllArticlesAsync(ct));
        }
        catch (NotFoundException notFound)
        {
            return NotFound(notFound.Message);
        }
        catch (Exception)
        {
            return BadRequest(ErrorMessages.BaseError);
        }
    }

    #region Admin Endpoints

    [HttpPost("publish")]
    [Authorize(Policy = AuthPolicy.Admin)]
    public async Task<IActionResult> PublishArticleAsync(BlogRequests.PublishArticle request, CancellationToken ct)
    {
        try
        {
            await service.PublishArticleAsync(User, request, ct);

            return Created();
        }
        catch (Exception)
        {
            return BadRequest(ErrorMessages.BaseError);
        }
    }

    #endregion
}