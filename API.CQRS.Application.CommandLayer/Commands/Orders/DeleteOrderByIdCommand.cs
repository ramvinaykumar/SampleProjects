using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace API.CQRS.Application.CommandLayer.Commands.Orders
{
    public class DeleteOrderByIdCommand : IRequest<int>
    {
        [Required]
        public int OrderId { get; set; }

        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteOrderByIdCommand, int>
        {
            private readonly IConfiguration _configuration;

            public DeleteProductByIdCommandHandler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<int> Handle(DeleteOrderByIdCommand command, CancellationToken cancellationToken)
            {
                var sql = "DELETE FROM Orders WHERE OrderId = @OrderId";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, new { OrderId = command.OrderId });
                    return result;
                }
            }
        }
    }
}
