using Application.Inventory.EFDBF.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Inventory.EFDBF.API.Controllers
{
    /// <summary>
    /// Products Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly TutorialsContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">TutorialsContext context</param>
        public ProductsController(TutorialsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/Products
        /// </summary>
        /// <param name="inStock">bool? inStock</param>
        /// <param name="skip">int? skip</param>
        /// <param name="take">int? take</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts(bool? inStock, int? skip, int? take)
        {
            var products = _context.Products.AsQueryable();

            if (inStock != null) // Adds the condition to check availability 
            {
                products = _context.Products.Where(i => i.AvailableQuantity > 0);
            }

            if (skip != null)
            {
                products = products.Skip((int)skip);
            }

            if (take != null)
            {
                products = products.Take((int)take);
            }

            return await products.ToListAsync();
        }

        /// <summary>
        /// Get Products by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        /// <summary>
        /// Update product by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <param name="products">Products products</param>
        /// <returns></returns>
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, Products products)
        {
            if (id != products.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Insert product data
        /// </summary>
        /// <param name="products">Products products</param>
        /// <returns></returns>
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts(Products products)
        {
            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = products.ProductId }, products);
        }

        /// <summary>
        /// DELETE product by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return products;
        }

        /// <summary>
        /// Products Exists
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
