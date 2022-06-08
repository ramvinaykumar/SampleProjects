using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace API.CQRS.Application.CommandLayer.Commands.Orders
{
    public class CreateUpdateOrderCommand : IRequest<int>
    {
        public int OrderId { get; set; }
        [Required]
        public string OrderDetails { get; set; }

        public class CreateUpdateOrderCommandHandler : IRequestHandler<CreateUpdateOrderCommand, int>
        {
            private readonly IConfiguration configuration;

            public CreateUpdateOrderCommandHandler(IConfiguration configuration)
            {
                this.configuration = configuration;
            }

            public async Task<int> Handle(CreateUpdateOrderCommand command, CancellationToken cancellationToken)
            {
                if (command.OrderId > 0)
                {
                    var sql = "Update Orders set OrderDetails = @OrderDetails Where OrderId = @OrderId";
                    using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        var result = await connection.ExecuteAsync(sql, command);
                        return result;
                    }
                }
                else
                {
                    var sql = "Insert into Orders (OrderDetails, IsActive, OrderedDate) VALUES (@OrderDetails, 1, GETDATE())";
                    using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        var result = await connection.ExecuteAsync(sql, new { OrderDetails = command.OrderDetails });
                        return result;
                    }
                }
            }
        }
    }
}
