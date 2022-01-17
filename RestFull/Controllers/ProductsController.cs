using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestFull.Data;
using RestFull.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFull.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProduct()
        {
            return await _context.Products.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
