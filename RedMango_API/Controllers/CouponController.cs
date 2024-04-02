using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedMango_API.Data;
using RedMango_API.Models;
using RedMango_API.Models.Dto;
using RedMango_API.Models.DTO;
using RedMango_API.Utility;
using System.Net;

namespace RedMango_API.Controllers
{
    [Route("api/Coupon")]
    [ApiController]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ApiResponse _response;
        private readonly IMapper _mapper;
        public CouponController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public async Task<IActionResult> GetCoupons()
        {
            try
            {
                _response.Result = await _db.Coupons.ToListAsync();
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.IsSuccess = false;
            }
            return BadRequest(_response);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public async Task<IActionResult> GetCouponById(int id)
        {
            try
            {
                Coupon coupon = await _db.Coupons.FirstOrDefaultAsync(u => u.Id == id);
                if(coupon == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { "Something wrong when get coupon by id" };
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                _response.Result = coupon;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.IsSuccess = false;
            }
            return BadRequest(_response);
        }

        [HttpGet("getByCode/{code}")]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public async Task<IActionResult> GetCouponByCode(string code)
        {
            try
            {
                Coupon coupon = await _db.Coupons.FirstOrDefaultAsync(u => u.Code == code);
                if (coupon == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { "Something wrong when get coupon by id" };
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                _response.Result = coupon;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.IsSuccess = false;
            }
            return BadRequest(_response);
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<ApiResponse>> CreateCoupon([FromForm] CouponCreateDTO couponDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Coupon coupon = _mapper.Map<Coupon>(couponDto);
                    _db.Coupons.Add(coupon);
                    await _db.SaveChangesAsync();

                    _response.Result = coupon;
                    _response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<ApiResponse>> UpdateCoupon(int id, [FromForm] CouponUpdateDTO couponDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (couponDto == null || id != couponDto.Id)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.ErrorMessages = new List<string>() { "Something wrong when update coupon" };
                        return BadRequest();
                    }
                    Coupon couponFromDb = await _db.Coupons.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
                    if (couponFromDb == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.ErrorMessages = new List<string>() { "Something wrong when update coupon" };
                        _response.IsSuccess = false;
                        return BadRequest(_response);
                    }
                    Coupon couponUpdate = _mapper.Map<Coupon>(couponDto);
                    _db.Coupons.Update(couponUpdate);
                    await _db.SaveChangesAsync();
                    _response.Result = couponUpdate;
                    _response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<ApiResponse>> DeleteCoupon(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Coupon coupon = _db.Coupons.First(u => u.Id == id);
                    _db.Coupons.Remove(coupon);
                    await _db.SaveChangesAsync();

                    _response.Result = true;
                    _response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}
