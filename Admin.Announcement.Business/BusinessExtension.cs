using Admin.Announcement.Business.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Admin.Announcement.Business
{
    [ExcludeFromCodeCoverage]
    public static class BusinessExtension
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            MapperConfiguration cfg = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            return services.AddMediatR(typeof(BusinessExtension).Assembly)
                           .AddSingleton(cfg.CreateMapper());
        }


    }
}