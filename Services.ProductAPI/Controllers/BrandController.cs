using Microsoft.AspNetCore.Mvc;
using Services.ProductAPI.Models.RequsetModels.BrandRequestModels;
using Services.ProductAPI.Services.Interfaces;

namespace Services.ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : Controller
{
    private readonly IBrandService _brandService;

    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateBrand(BrandCreateRequestModel model)
    {
        var response = await _brandService.CreateBrand(model);

        if (response.StatusCode == Models.StatusCode.InternalServerError) return StatusCode(500, response);

        if (response.StatusCode == Models.StatusCode.BadRequest) return BadRequest(response);

        return StatusCode(201, response);
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllBrands()
    {
        var response = await _brandService.GetBrands();
        
        if (response.StatusCode == Models.StatusCode.InternalServerError) return StatusCode(500, response);

        return Json(response);
    }
}