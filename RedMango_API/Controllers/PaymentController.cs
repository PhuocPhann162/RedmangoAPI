using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedMango_API.Data;
using RedMango_API.Models;
using Stripe;
using System.Net;

namespace RedMango_API.Controllers
{
    [Route("api/stripePayment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        protected ApiResponse _response;
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        public PaymentController(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
            _response = new();
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> MakePayment(string userId)
        {
            try
            {
                ShoppingCart shoppingCart = _db.ShoppingCarts.
                    Include(u => u.CartItems).
                    ThenInclude(u => u.MenuItem).FirstOrDefault(u => u.UserId == userId);
                if (shoppingCart == null || shoppingCart.CartItems == null || shoppingCart.CartItems.Count() == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                #region Create Payment Intent
                StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
                shoppingCart.CartTotal = shoppingCart.CartItems.Sum(u => u.Quantity * u.MenuItem.Price);

                PaymentIntentCreateOptions options = new()
                {
                    Amount = (int)(shoppingCart.CartTotal * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                };
                PaymentIntentService service = new();
                PaymentIntent response = service.Create(options);

                shoppingCart.StripePaymentIntentId = response.Id;
                shoppingCart.ClientSecret = response.ClientSecret;

                #endregion

                _response.Result = shoppingCart;
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
    }
}
