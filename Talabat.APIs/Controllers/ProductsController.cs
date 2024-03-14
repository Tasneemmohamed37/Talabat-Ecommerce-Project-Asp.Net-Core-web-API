using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities.Product;
using Talabat.Core.Interfaces;
using Talabat.Core.Specification;
using Talabat.Core.Specification.Products;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [ProducesResponseType(typeof(IReadOnlyList<Pagination<ProductToReturnDto>>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>>> GetAllProducts([FromQuery]ProductSpecParams specParams) // use attribute from query becouse by defualt search for obj param in body , and get request not have body
        {
            var spec = new ProductWithTypeAndBrandSpecifications(specParams);

            IReadOnlyList<Product> products = await _productRepo.GetAllWithSpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var countSpec = new ProductWithFilterationForCountSpecification(specParams);

            var count = await _productRepo.GetCountWithSpecAsync(countSpec);
            
            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex , specParams.PageSize , count ,data));
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
