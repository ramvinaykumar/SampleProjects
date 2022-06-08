using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace API.CQRS.Application.CommandLayer.Commands.Products
{
    public class CreateUpdateProductCommand : IRequest<int>
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }

        public class CreateUpdateProductCommandHandler : IRequestHandler<CreateUpdateProductCommand, int>
        {
            private readonly IConfiguration configuration;

            public CreateUpdateProductCommandHandler(IConfiguration configuration)
            {
                this.configuration = configuration;
            }

            public async Task<int> Handle(CreateUpdateProductCommand command, CancellationToken cancellationToken)
            {
                if (command.ProductId > 0)
                {
                    var sql = "Update Products set ProductName = @ProductName, Category = @Category, Color = @Color, AvailableQuantity = @AvailableQuantity, UnitPrice = @UnitPrice Where ProductId = @ProductId";
                    using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        var result = await connection.ExecuteAsync(sql, command);
                        return result;
                    }
                }
                else
                {
                    var sql = "Insert into Products (ProductName, Category,  Color, AvailableQuantity, UnitPrice, IsActive, CreatedDate) VALUES (@ProductName, @Category, @Color, @AvailableQuantity, @UnitPrice, 1, GETDATE())";
                    using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        var result = await connection.ExecuteAsync(sql, new
                        {
                            ProductName = command.ProductName,
                            Category = command.Category,
                            Color = command.Color,
                            AvailableQuantity = command.AvailableQuantity,
                            UnitPrice = command.UnitPrice
                        });
                        return result;
                    }
                }
            }
        }
    }
}
