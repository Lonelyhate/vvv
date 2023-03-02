using Microsoft.AspNetCore.Mvc;
using Services.ProductAPI.Models.RequsetModels;
using Services.ProductAPI.Models.ViewModels;
using Services.ProductAPI.Services.Interfaces;

namespace Services.ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromForm] ProductFormViewModel model)
    {
        var response = await _productService.CreateProduct(model);

        if (response.StatusCode == Models.StatusCode.InternalServerError) return StatusCode(500, response);

        if (response.StatusCode == Models.StatusCode.BadRequest) return BadRequest(response);

        return StatusCode(201, response);
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllProduct(string? category, string? orderBy, int? take)
    {
        var response = await _productService.GetAllProducts(category, orderBy, take);

        if (response.StatusCode == Models.StatusCode.NoContent) return Json(response);

        if (response.StatusCode == Models.StatusCode.InternalServerError) return StatusCode((int)response.StatusCode, response);

        return Json(response);
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var response = await _productService.GetProductById(id);

        if (response.StatusCode == Models.StatusCode.NotFound) return NotFound(response);
        
        if (response.StatusCode == Models.StatusCode.InternalServerError) return StatusCode((int)response.StatusCode, response);

        return Json(response);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var response = await _productService.DeleteProduct(id);
        
        if (response.StatusCode == Models.StatusCode.NotFound) return NotFound(response);
        
        if (response.StatusCode == Models.StatusCode.InternalServerError) return StatusCode((int)response.StatusCode, response);

        return Json(response);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateProduct(ProductUpdateRequestModel model)
    {
        var response = await _productService.UpdateProduct(model);

        if (response.StatusCode == Models.StatusCode.BadRequest) return StatusCode(400, response);
        
        if (response.StatusCode == Models.StatusCode.InternalServerError) return StatusCode((int)response.StatusCode, response);

        return Json(response);
    }
}