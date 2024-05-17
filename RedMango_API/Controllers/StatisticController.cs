using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/statistic")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private ApplicationDbContext _db;
        protected ApiResponse _response;
        private readonly IMapper _mapper;
        public StatisticController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new();
        }

        //[Authorize(Roles = SD.Role_Admin)]
        [HttpGet("revenue")]
        public async Task<ActionResult<ApiResponse>> GetRevenueStatistic(string type, int year, int month, int? endYear)
        {
            try
            {
                List<OrderHeader> lstOrders = await _db.OrderHeader.ToListAsync();
                if (type == "daily")
                {
                    int daysInMonth = DateTime.DaysInMonth(year, month);
                    RevenueStatisticDTO revenueStatisticDTO = new()
                    {
                        DaysInMonth = daysInMonth,
                        Label = "Daily Revenue Statistic",
                        RevenueData = new(),
                    };

                    for (int i = 1; i <= daysInMonth; i++)
                    {
                        DateTime currentDate = new DateTime(year, month, i);
                        IEnumerable<OrderHeader> ordersFromDb = lstOrders.Where(u => u.OrderDate.Date == currentDate.Date);
                        if (ordersFromDb.Count() > 0)
                        {
                            double totalForDay = ordersFromDb.Sum(o => o.OrderTotal - o.DiscountAmount);
                            revenueStatisticDTO.RevenueData.Add(totalForDay);
                        }
                        else
                        {
                            revenueStatisticDTO.RevenueData.Add(0);
                        }
                    }
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = revenueStatisticDTO;
                    return Ok(_response);
                }
                else if (type == "monthly")
                {
                    RevenueStatisticDTO revenueStatisticDTO = new()
                    {
                        Label = "Monthly Revenue Statistic",
                        RevenueData = new(),
                    };

                    for (int i = 1; i <= 12; i++)
                    {
                        IEnumerable<OrderHeader> ordersFromDb = lstOrders.Where(u => u.OrderDate.Date.Month == i && u.OrderDate.Date.Year == year);
                        if (ordersFromDb.Count() > 0)
                        {
                            double totalForMonth = ordersFromDb.Sum(o => o.OrderTotal - o.DiscountAmount);
                            revenueStatisticDTO.RevenueData.Add(totalForMonth);
                        }
                        else
                        {
                            revenueStatisticDTO.RevenueData.Add(0);
                        }
                    }
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = revenueStatisticDTO;
                    return Ok(_response);
                }
                else if (type == "yearly" && endYear.HasValue)
                {
                    RevenueStatisticDTO revenueStatisticDTO = new()
                    {
                        Label = "Yearly Revenue Statistic",
                        RevenueData = new(),
                    };

                    for (int i = year; i <= endYear; i++)
                    {
                        IEnumerable<OrderHeader> ordersFromDb = lstOrders.Where(u => u.OrderDate.Date.Year == i);
                        if (ordersFromDb.Count() > 0)
                        {
                            double totalForYear = ordersFromDb.Sum(o => o.OrderTotal - o.DiscountAmount);
                            revenueStatisticDTO.RevenueData.Add(totalForYear);
                        }
                        else
                        {
                            revenueStatisticDTO.RevenueData.Add(0);
                        }
                    }
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = revenueStatisticDTO;
                    return Ok(_response);
                }

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet("order")]
        public async Task<ActionResult<ApiResponse>> GetOrderStatistic()
        {
            try
            {

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
