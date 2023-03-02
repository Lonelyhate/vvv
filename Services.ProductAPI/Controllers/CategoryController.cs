using Microsoft.AspNetCore.Mvc;
using Services.ProductAPI.Models.RequsetModels.CategoryRequestModels;
using Services.ProductAPI.Services.Interfaces;

namespace Services.ProductAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CategoryCreate(CategoryCreateRequestModel model)
    {
        var response = await _categoryService.CategoryCreate(model);
        
        if (response.StatusCode == Models.StatusCode.InternalServerError) return StatusCode(500, response);

        if (response.StatusCode == Models.StatusCode.BadRequest) return BadRequest(response);

        return StatusCode(201, response);
    }

    [HttpGet("getall")]
    public async Task<IActionResult> CategoryGetAll()
    {
        var response = await _categoryService.CategoryGetAll();
        
        if (response.StatusCode == Models.StatusCode.InternalServerError) return StatusCode(500, response);

        return Ok(response);
    }
}