using API.CQRS.Application.CommandLayer.Commands.Orders;
using API.CQRS.Application.CommandLayer.Queries.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.CQRS.Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        /// <summary>
        /// Save newly added order to database
        /// </summary>
        /// <param name="command">CreateUpdateOrderCommand command</param>
        /// <returns></returns>
        [HttpPost(nameof(SaveOrderData))]
        public async Task<IActionResult> SaveOrderData(CreateUpdateOrderCommand command) => Ok(await Mediator.Send(command));

        /// <summary>
        /// Delete Order from the Orders Table
        /// </summary>
        /// <param name="command">DeleteOrderByIdCommand command</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteOrder(DeleteOrderByIdCommand command) => Ok(await Mediator.Send(command));

        /// <summary>
        /// Fetch all data from the Orders table.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllOrders() => Ok(await Mediator.Send(new GetAllOrdersQuery()));

    }
}
