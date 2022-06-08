using CardPayment.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CardPayment.API.Controllers
{
    /// <summary>
    /// Card detail api controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CardDetailController : ControllerBase
    {
        /// <summary>
        /// Readonly variable of db context
        /// </summary>
        private readonly CardDbContext _context;

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="context"></param>
        public CardDetailController(CardDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get method which will return card detail list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardDetail>>> GetCardDetails()
        {
            return await _context.CardDetails.ToListAsync();
        }

        /// <summary>
        /// Get card detail by id (api/CardDetail/5)
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CardDetail>> GetCardDetail(int id)
        {
            var CardDetail = await _context.CardDetails.FindAsync(id);

            if (CardDetail == null)
            {
                return NotFound();
            }

            return CardDetail;
        }

        // PUT: api/CardDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardDetail(int id, CardDetail cardDetail)
        {
            if (id != cardDetail.CardDetailId)
            {
                return BadRequest();
            }

            _context.Entry(cardDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardDetailExists(id))
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

        // POST: api/CardDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CardDetail>> PostCardDetail(CardDetail cardDetail)
        {
            _context.CardDetails.Add(cardDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCardDetail", new { id = cardDetail.CardDetailId }, cardDetail);
        }

        // DELETE: api/CardDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardDetail(int id)
        {
            var CardDetail = await _context.CardDetails.FindAsync(id);
            if (CardDetail == null)
            {
                return NotFound();
            }

            _context.CardDetails.Remove(CardDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Check whether card detail exists or not
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns true if exists</returns>
        private bool CardDetailExists(int id)
        {
            return _context.CardDetails.Any(e => e.CardDetailId == id);
        }
    }
}
