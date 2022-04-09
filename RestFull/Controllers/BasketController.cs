using System;
using System.Linq;
using System.Threading.Tasks;
using RestFull.Data;
//using RestFull.DTOs;
using RestFull.Entities;
//using RestFull.Extensions;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RestFull.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreContext _context;
        public BasketController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Basket>> GetBasket()
        {
            var basket = await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);

            if (basket == null) return NotFound();

            return basket;
        }

        [HttpPost]
        public ActionResult AddItemToBasket(int productId, int quantity)
        {
            var basket = await RetrieveBasket(GetBuyerId());

            if (basket == null) basket = CreateBasket();

            var product = await _context.Products.FindAsync(productId);

            if (product == null) return NotFound(); //BadRequest(new ProblemDetails { Title = "Product not found" });

            basket.AddItem(product, quantity);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return StatusCode(201); //return CreatedAtRoute("GetBasket", basket.MapBasketToDto());

            //BadRequest(new ProblemDetails { Title = "Problem saving item to basket" });
        }

        [HttpDelete]
        public ActionResult RemoveBasketItem(int productId, int quantity)
        {
            //var basket = await RetrieveBasket(GetBuyerId());

            //if (basket == null) return NotFound();

            //basket.RemoveItem(productId, quantity);

            //var result = await _context.SaveChangesAsync() > 0;

            //if (result)
            return Ok();

            //return BadRequest(new ProblemDetails { Title = "Problem removing item from the basket" });
        }

        //private async Task<Basket> RetrieveBasket(string buyerId)
        //{
        //    if (string.IsNullOrEmpty(buyerId))
        //    {
        //        Response.Cookies.Delete("buyerId");
        //        return null;
        //    }

        //    return await _context.Baskets
        //        .Include(i => i.Items)
        //        .ThenInclude(p => p.Product)
        //        .FirstOrDefaultAsync(x => x.BuyerId == buyerId);
        //}

        //private Basket CreateBasket()
        //{
        //    var buyerId = User.Identity?.Name;
        //    if (string.IsNullOrEmpty(buyerId))
        //    {
        //        buyerId = Guid.NewGuid().ToString();
        //        var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
        //        Response.Cookies.Append("buyerId", buyerId, cookieOptions);
        //    }
        //    var basket = new Basket { BuyerId = buyerId };
        //    _context.Baskets.Add(basket);
        //    return basket;
        //}

        //private string GetBuyerId()
        //{
        //    return User.Identity?.Name ?? Request.Cookies["buyerId"];
        //}
    }
}