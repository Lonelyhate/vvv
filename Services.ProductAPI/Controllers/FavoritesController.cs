using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Services.ProductAPI.Models.RequsetModels.FavoritesRequestModels;
using Services.ProductAPI.Services.Interfaces;

namespace Services.ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoritesController : Controller
{
    private readonly IFavoritesService _favoritesService;

    public FavoritesController(IFavoritesService favoritesService)
    {
        _favoritesService = favoritesService;
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddToFavorites(FavoritesAddRequestModel model)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var response = await _favoritesService.AddToFavorites(model, Int32.Parse(userId));

        if (response.StatusCode == Models.StatusCode.BadRequest) return BadRequest(response);

        if (response.StatusCode == Models.StatusCode.InternalServerError) return StatusCode(500, response);

        return StatusCode(201, response);
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetFavorites()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var response = await _favoritesService.GetFavoritesById(Int32.Parse(userId));

        if (response.StatusCode == Models.StatusCode.InternalServerError) return StatusCode(500, response);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFavorites(int id)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var response = await _favoritesService.DeleteFavoritesById(id, Int32.Parse(userId));

        if (response.StatusCode == Models.StatusCode.BadRequest) return BadRequest(response);

        if (response.StatusCode == Models.StatusCode.InternalServerError) return StatusCode(500, response);

        return Ok(response);
    }
}