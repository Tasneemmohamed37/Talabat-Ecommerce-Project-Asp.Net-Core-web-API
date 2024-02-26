using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Product;
using Talabat.Core.Interfaces;
using Talabat.Core.Specification;
using Talabat.Core.Specification.Products;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo , IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetAllProducts()
        {
            var spec = new ProductWithTypeAndBrandSpecifications();
            IEnumerable<Product> products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductByIdAsync(int id)
        {
                var spec = new ProductWithTypeAndBrandSpecifications(id);
                Product prd = await _productRepo.GetByIdWithSpecAsync(spec);
                if(prd == null)
                    return NotFound(new ApiResponse(404));
                return Ok(_mapper.Map<Product , ProductToReturnDto>(prd));
        }

       
    }
}
