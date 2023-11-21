using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedMango_API.Data;
using RedMango_API.Models;
using RedMango_API.Models.Dto;
using RedMango_API.Utility;
using System.Net;

namespace RedMango_API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _db; 
        protected ApiResponse _response;
        private readonly IMapper _mapper;
        public OrderController(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetOrders(string? userId)
        {
            try
            {
                var orderHeaders = _db.OrderHeader.Include(u => u.OrderDetails).
                    ThenInclude(u => u.MenuItem).
                    OrderByDescending(u => u.OrderHeaderId);
                if(!string.IsNullOrEmpty(userId))
                {
                    _response.Result = orderHeaders.Where(u => u.ApplicationUserId == userId);
                    
                }
                else
                {
                    _response.Result = orderHeaders;
                }
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }

            return Ok(_response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse>> GetOrder(int id)
        {
            try
            {
                if(id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var orderHeaders = _db.OrderHeader.Include(u => u.OrderDetails).
                    ThenInclude(u => u.MenuItem).
                    Where(u => u.OrderHeaderId == id);
                if (orderHeaders == null)
                {
                    _response.StatusCode=HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                _response.Result = orderHeaders;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }

            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateOrder([FromBody]OrderHeaderCreateDTO orderHeaderDTO)
        {
            try
            {
                var order = _mapper.Map<OrderHeader>(orderHeaderDTO);
                order.Status = String.IsNullOrEmpty(orderHeaderDTO.Status) ? SD.Status_Pending : orderHeaderDTO.Status;

                if(ModelState.IsValid)
                {
                    _db.OrderHeader.Add(order);
                    _db.SaveChanges();
                    foreach(var orderDetailDTO in orderHeaderDTO.OrderDetailsDTO)
                    {
                        var orderDetails = _mapper.Map<OrderDetails>(orderDetailDTO);
                        _db.OrderDetails.Add(orderDetails);
                    }
                    _db.SaveChanges();
                    _response.Result = order;
                    order.OrderDetails = null;
                    _response.StatusCode = HttpStatusCode.Created;
                    return Ok(_response);
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }

            return Ok(_response);
        }
    }
}
