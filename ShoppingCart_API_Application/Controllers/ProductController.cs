using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart_API_Application.Data;
using ShoppingCart_API_Application.Models;
using ShoppingCart_API_Application.Models.DTO.product;
using ShoppingCart_API_Application.Repository.Interfaces;
using System.Net;

namespace ShoppingCart_API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        public ProductController(ILogger<ProductController> logger,IProductRepository db,IMapper mapper)
        {
            _repository = db;   
            _logger=logger;
            _mapper=mapper;
            this._response = new APIResponse();
        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            try
            {
                _logger.LogInformation("Getting all products");
                IEnumerable<Product> productlist = await _repository.GetAll();
                _response.Result = _mapper.Map<List<ProductDTO>>(productlist);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("There has any Error in your program");
                _response.IsSuccess=false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            try
            {
              if ( id==0 || id<0)
              {
                _logger.LogError("Get product Error with id "  + id);
                return BadRequest();
              }
              var item = await _repository.Get(u => u.Id == id);

              if (item==null)
              {
                _logger.LogInformation("For Product where you want to find is Empty please try again");
                return NotFound();


              }
              _response.Result = _mapper.Map<ProductDTO>(item);
              _response.StatusCode = HttpStatusCode.OK;
              _response.IsSuccess = true;
               return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }
        [HttpPost]
        [Authorize(Roles ="admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateProduct([FromBody]ProductCreateDTO productDTO)
        {
            try
            {
                if (await _repository.Get(i => i.Name.ToLower() == productDTO.Name.ToLower()) != null)
                {
                    _logger.LogInformation("Product already exist you can not add another product where name equals");
                    return BadRequest();
                }
                if (productDTO == null)
                {
                    _logger.LogInformation("Product can not null ");
                    return BadRequest(productDTO);
                }
                if (productDTO.Id > 0)
                {
                    _logger.LogInformation("Product Id  not null ");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                Product model = _mapper.Map<Product>(productDTO);
                await _repository.Create(model);
                _logger.LogInformation("We are add data base in data");
                _response.Result = _mapper.Map<ProductDTO>(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        [Authorize(Roles ="CUSTOME")]
        public async Task<ActionResult<APIResponse>> DeleteProduct(int id)
        {
            try
            {
                  if (id == 0)
                  {
                    return BadRequest();
                  }
                   var product = await _repository.Get(i=>i.Id==id);
                  if (product == null)
                  {
                     return NotFound();
                  }
                  _logger.LogInformation($" We are Delete this  {product} 'data' ");
                  await _repository.Remove(product);
                  _response.StatusCode = HttpStatusCode.OK;
                  _response.IsSuccess = true;
                  _response.Result="You Deleted  " + product.Name +"  data seccesfully " ;
                  return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }
        [HttpPut("{id:int}", Name = "ProductUpdate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateProduct(int id, [FromBody] ProductDTO productDTO)
        {
            try
            {
                if (productDTO == null || id != productDTO.Id)
                {
                    _logger.LogInformation("Product Empty or Product Id equel to product");
                    return BadRequest();
                }
                _logger.LogInformation($" Updating this {productDTO.Id} data");
                //Product model = new Product()
                //{
                //    Amenity = productDTO.Amenity,
                //    Id = productDTO.Id,
                //    Details = productDTO.Details,
                //    ImageUrl = productDTO.ImageUrl,
                //    Occupancy = productDTO.Occupancy,
                //    Rate = productDTO.Rate,
                //    Sqft = productDTO.Sqft,
                //    Name = productDTO.Name
                //};
                Product model = _mapper.Map<Product>(productDTO);
                await _repository.Update(model);
                return NoContent();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }
        [HttpPatch("{id:int}", Name = "UpdatePartialProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdatepartialProduct(int id, JsonPatchDocument<ProductUpdateDTO> patch)
        {

            try
            {
                if (patch == null || id == 0)
                {
                    return BadRequest();
                }
                var productDTO = await _repository.Get(u => u.Id == id);
                if (patch == null)
                {
                    return BadRequest();
                }
                ProductUpdateDTO p = new()
                {
                    Amenity = productDTO.Amenity,
                    Id = productDTO.Id,
                    Details = productDTO.Details,
                    ImageUrl = productDTO.ImageUrl,
                    Occupancy = productDTO.Occupancy,
                    Rate = productDTO.Rate,
                    Sqft = productDTO.Sqft,
                    Name = productDTO.Name
                };
                ProductUpdateDTO productUpdateDTO = _mapper.Map<ProductUpdateDTO>(productDTO);
                patch.ApplyTo(p, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Product model = new Product()
                {
                    Amenity = p.Amenity,
                    Id = p.Id,
                    Details = p.Details,
                    ImageUrl = p.ImageUrl,
                    Occupancy = p.Occupancy,
                    Rate = p.Rate,
                    Sqft = p.Sqft,
                    Name = p.Name
                };
                await _repository.Update(model);
                return NoContent();
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }
    }
}
