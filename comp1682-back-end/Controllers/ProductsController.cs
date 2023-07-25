using Microsoft.AspNetCore.Mvc;

using comp1682_back_end.Data;
using comp1682_back_end.Models;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using comp1682_back_end.DTOs;
using comp1682_back_end.Repositories.Interfaces;

namespace comp1682_back_end.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
      var products = await _unitOfWork.Products.GetAllProducts();
      return _mapper.Map<List<ProductDto>>(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
      var product = await _unitOfWork.Products.GetProductById(id);

      if (product == null)
      {
        return NotFound();
      }

      return _mapper.Map<ProductDto>(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> PostProduct(ProductDto productDto)
    {
      var product = _mapper.Map<Product>(productDto);
      var addedProduct = await _unitOfWork.Products.AddProduct(product);
      await _unitOfWork.SaveChangesAsync();
      return CreatedAtAction(nameof(GetProduct), new { id = addedProduct.Id }, _mapper.Map<ProductDto>(addedProduct));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, ProductDto productDto)
    {
      if (id != productDto.Id)
      {
        return BadRequest();
      }

      var existingProduct = await _unitOfWork.Products.GetProductById(id);
      if (existingProduct == null)
      {
        return NotFound();
      }

      _mapper.Map(productDto, existingProduct);
      await _unitOfWork.SaveChangesAsync();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
      var product = await _unitOfWork.Products.GetProductById(id);
      if (product == null)
      {
        return NotFound();
      }

      var deleted = await _unitOfWork.Products.DeleteProduct(id);
      if (deleted)
      {
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
      }
      else
      {
        // Something went wrong with deletion
        return StatusCode(500);
      }
    }
  }
}
