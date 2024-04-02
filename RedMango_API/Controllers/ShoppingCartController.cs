﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedMango_API.Data;
using RedMango_API.Models;
using RedMango_API.Models.Dto;
using System.Net;

namespace RedMango_API.Controllers
{
    [Route("api/shoppingCart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        protected ApiResponse _response;
        public ShoppingCartController(ApplicationDbContext db)
        {
            _response = new();
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetShoppingCart(string userId)
        {
            try
            {
                ShoppingCart shoppingCart;
                if (string.IsNullOrEmpty(userId))
                {
                    shoppingCart = new();
                }
                else
                {
                    shoppingCart = _db.ShoppingCarts.
                        Include(u => u.CartItems).ThenInclude(u => u.MenuItem).
                        FirstOrDefault(u => u.UserId == userId);
                }

                if (shoppingCart == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                if (shoppingCart.CartItems != null && shoppingCart.CartItems.Count > 0)
                {
                    shoppingCart.CartTotal = shoppingCart.CartItems.Sum(u => u.Quantity * u.MenuItem.Price);
                }

                if (!string.IsNullOrEmpty(shoppingCart.CouponCode))
                {
                    Coupon coupon = await _db.Coupons.FirstOrDefaultAsync(u => u.Code == shoppingCart.CouponCode);
                    if (coupon != null && shoppingCart.CartTotal > coupon.MinAmount)
                    {
                        shoppingCart.CartTotal -= coupon.DiscountAmount;
                        shoppingCart.Discount = coupon.DiscountAmount;
                    }
                }

                _response.Result = shoppingCart;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }

            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddOrUpdateItemInCart(string userId, int menuItemId, int updateQuantityBy)
        {
            // Shopping cart will have one entry per user id, even if a user has many items in cart.
            // Cart items will have all the items in shopping cart for a user. 
            // updateQuantityBy will have count by with an items quantity needs to be updated.
            // if it is -1 that means we have lower a count if it is 5 it means we have to add 5 count to existing count.
            // if updateQuantityBy by is 0, item will be removed.

            // when a user adds a new item to a new shopping cart for the first time 
            // when a user adds a new item to an existing shopping cart (basically user has other items in cart) 
            // when a user updates an existing item count 
            // when a user removes an existing item

            try
            {

                ShoppingCart shoppingCart = _db.ShoppingCarts.Include(u => u.CartItems).FirstOrDefault(u => u.UserId == userId);
                MenuItem menuItem = _db.MenuItems.FirstOrDefault(u => u.Id == menuItemId);
                if (menuItem == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (shoppingCart == null && updateQuantityBy > 0)
                {
                    // create a shopping cart && add cartItem

                    ShoppingCart newCart = new() { UserId = userId };
                    _db.ShoppingCarts.Add(newCart);
                    _db.SaveChanges();

                    CartItem newCartItem = new()
                    {
                        MenuItemId = menuItemId,
                        Quantity = updateQuantityBy,
                        ShoppingCartId = newCart.Id,
                        MenuItem = null
                    };
                    _db.CartItems.Add(newCartItem);
                    _db.SaveChanges();
                }
                else
                {
                    // shopping cart exists 
                    CartItem cartItemInCart = shoppingCart.CartItems.FirstOrDefault(u => u.MenuItemId == menuItemId);
                    if (cartItemInCart == null)
                    {
                        // item does not exist in shoppingCart
                        CartItem newCartItem = new()
                        {
                            MenuItemId = menuItemId,
                            Quantity = updateQuantityBy,
                            ShoppingCartId = shoppingCart.Id,
                            MenuItem = null
                        };
                        _db.CartItems.Add(newCartItem);
                        _db.SaveChanges();
                    }
                    else
                    {
                        // item already exist in the cart and we have to update quantity 
                        int newQuantity = cartItemInCart.Quantity + updateQuantityBy;
                        if (updateQuantityBy == 0 || newQuantity <= 0)
                        {
                            // remove cart item from cart and if it is the only item then remove cart 
                            _db.CartItems.Remove(cartItemInCart);
                            if (shoppingCart.CartItems.Count() == 1)
                            {
                                shoppingCart.CartTotal = 0;
                                _db.ShoppingCarts.Remove(shoppingCart);
                            }
                            _db.SaveChanges();
                        }
                        else
                        {
                            cartItemInCart.Quantity = newQuantity;
                            _db.SaveChanges();
                        }
                    }
                }
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

        [HttpPost("applyCoupon")]
        public async Task<ActionResult<ApiResponse>> ApplyCoupon([FromBody]ShoppingCartDTO cartDto)
        {
            try
            {
                var cartFromDb = await _db.ShoppingCarts.FirstAsync(u => u.UserId == cartDto.UserId);
                if(!string.IsNullOrEmpty(cartFromDb.CouponCode))
                {
                    cartFromDb.CouponCode = "";
                }
                else
                {
                    cartFromDb.CouponCode = cartDto.CouponCode;
                }
                _db.ShoppingCarts.Update(cartFromDb);
                await _db.SaveChangesAsync();
                _response.Result = cartFromDb;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.IsSuccess = false;
            }
            return BadRequest(_response);
        }
    }
}
