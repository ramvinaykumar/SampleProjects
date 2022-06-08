using API.CQRS.Application.CommandLayer.Commands.Products;
using API.CQRS.Application.CommandLayer.Queries.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.CQRS.Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        /// <summary>
        /// Save newly added product to database
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost(nameof(SaveProductData))]
        public async Task<IActionResult> SaveProductData(CreateUpdateProductCommand command) => Ok(await Mediator.Send(command));

        /// <summary>
        /// Delete Product from the Products Table
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteProduct))]
        public async Task<IActionResult> DeleteProduct(DeleteProductByIdCommand command) => Ok(await Mediator.Send(command));
        
        /// <summary>
        /// Fetch all Product Data from the Database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts() => Ok(await Mediator.Send(new GetAllProductsQuery()));
    }
}
