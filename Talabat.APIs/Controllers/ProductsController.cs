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
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Get All Products

        [ProducesResponseType(typeof(IReadOnlyList<Pagination<ProductToReturnDto>>), StatusCodes.Status200OK)]
        [HttpGet("AllProducts")]
        public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>>> GetAllProducts([FromQuery]ProductSpecParams specParams) // use attribute from query becouse by defualt search for obj param in body , and get request not have body
        {
            var spec = new ProductWithTypeAndBrandSpecifications(specParams);

            IReadOnlyList<Product> products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var countSpec = new ProductWithFilterationForCountSpecification(specParams);

            var count = await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(countSpec);
            
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
            Product prd = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);
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
            IReadOnlyList<Product_Type> types = await _unitOfWork.Repository<Product_Type>().GetAllAsync();
            return Ok(types);
        }
        #endregion

        #region Get All Product_brands
        [ProducesResponseType(typeof(IReadOnlyList<Product_Brand>), StatusCodes.Status200OK)]
        [HttpGet("productBrands")]
        public async Task<ActionResult<IReadOnlyList<Product_Brand>>> GetAllProductBrands()
        {
            IReadOnlyList<Product_Brand> brands = await _unitOfWork.Repository<Product_Brand>().GetAllAsync();
            return Ok(brands);
        }
        #endregion
    }
}
