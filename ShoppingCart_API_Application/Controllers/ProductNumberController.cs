using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart_API_Application.Models;
using ShoppingCart_API_Application.Models.DTO.product;
using ShoppingCart_API_Application.Models.DTO.productnumber;
using ShoppingCart_API_Application.Repository.Interfaces;
using System.Net;

namespace ShoppingCart_API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductNumberController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IProductNumberRepository _repo; 
        private readonly ILogger<ProductNumberController> _logger;
        private readonly IMapper _mapper;
        public ProductNumberController(IProductNumberRepository repository, ILogger<ProductNumberController> logger, IMapper mapper)
        {
            _response = new APIResponse();
            _repo = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            try
            {
                _logger.LogInformation("Getting all products");
                IEnumerable<ProductNumber> productlist = await _repo.GetAll();
                _response.Result = _mapper.Map<List<ProductNumberDTO>>(productlist);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("There has any Error in your program");
                _response.IsSuccess = false;
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
                if (id == 0 || id < 0)
                {
                    _logger.LogError("Get product Error with id " + id);
                    return BadRequest();
                }
                var item = await _repo.Get(u => u.ProductNo == id);

                if (item == null)
                {
                    _logger.LogInformation("For Product where you want to find is Empty please try again");
                    return NotFound();
                }
                _response.Result = _mapper.Map<ProductNumberDTO>(item);
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateProduct([FromBody] ProductNumberCreateDTO productDTO)
        {
            try
            {
                if (await _repo.Get(i => i.ProductNo  == productDTO.ProductNo) != null)
                {
                    _logger.LogInformation("Product already exist you can not add another product where name equals");
                    return BadRequest(ModelState);
                }
                if (productDTO == null)
                {
                    _logger.LogInformation("Product can not null ");
                    return BadRequest(productDTO);
                }
                if (productDTO.ProductNo > 0)
                {
                    _logger.LogInformation("Product Id  not null ");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                ProductNumber productnumber = _mapper.Map<ProductNumber>(productDTO);
                await _repo.Create(productnumber);
                _logger.LogInformation("We are add data base in data");
                _response.Result = _mapper.Map<ProductNumberDTO>(productnumber);
                _response.StatusCode = HttpStatusCode.Created;
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
    }
}
