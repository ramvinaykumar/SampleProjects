using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace API.CQRS.Application.CommandLayer
{
    public static class DependencyInjection
    {
        #region Services Injection
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
        #endregion

    }
}
