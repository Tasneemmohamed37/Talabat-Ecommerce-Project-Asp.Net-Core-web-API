using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        private readonly IGenericRepository<Product_Type> _typeRepo;
        private readonly IGenericRepository<Product_Brand> _brandRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productRepo,
            IGenericRepository<Product_Type> typeRepo,
            IGenericRepository<Product_Brand> brandRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _typeRepo = typeRepo;
            _brandRepo = brandRepo;
        }

        #region Get All Products

        [ProducesResponseType(typeof(IReadOnlyList<ProductToReturnDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetAllProducts(string? sort = null , int? brandId = null , int? typeId = null)
        {
            var spec = new ProductWithTypeAndBrandSpecifications(sort , brandId , typeId);
            IReadOnlyList<Product> products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        #endregion

        #region Get Product By Id Async

        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)] // used to improve swagger documentation
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecifications(id);
            Product prd = await _productRepo.GetByIdWithSpecAsync(spec);
            if (prd == null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product, ProductToReturnDto>(prd));
        }

        #endregion

        #region Get All Product_types
        [ProducesResponseType(typeof(IReadOnlyList<Product_Type>), StatusCodes.Status200OK)]
        [HttpGet("productTypes")]
        public async Task<ActionResult<IReadOnlyList<Product_Type>>> GetAllProductTypes()
        {
            IReadOnlyList<Product_Type> types = await _typeRepo.GetAllAsync();
            return Ok(types);
        }
        #endregion

        #region Get All Product_brands
        [ProducesResponseType(typeof(IReadOnlyList<Product_Brand>), StatusCodes.Status200OK)]
        [HttpGet("productBrands")]
        public async Task<ActionResult<IReadOnlyList<Product_Brand>>> GetAllProductBrands()
        {
            IReadOnlyList<Product_Brand> brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }
        #endregion
    }
}
