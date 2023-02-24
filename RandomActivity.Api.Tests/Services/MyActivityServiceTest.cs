using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using RandomActivity.API.Models;
using RandomActivity.API.Services;
using System.Net;
using System.Net.Http.Json;

namespace RandomActivity.Api.Tests.Services
{
    public class MyActivityServiceTests
    {
        [Test]
        public async Task TestSuccess()
        {
            using var application = new WebApplicationFactory<Program>();

            var client = application.CreateClient();

            var activity = await client.GetFromJsonAsync<MyActivityDto>("/MyActivity");

            Assert.IsNotNull(activity);
        }

        [Test]
        public async Task TestFailureReturnNull()
        {
            //mocking HttpClient with custom reponse
            HttpResponseMessage httpResponseMessage = new()
            {
                StatusCode = HttpStatusCode.InternalServerError
            };
            var mockHttpClient = GetMockedHttpClient(httpResponseMessage);

            // to inject the mock http client to services
            using var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        ServiceDescriptor serviceDescriptor = new(typeof(IMyActivityService),
                                typeof(MyActivityService), ServiceLifetime.Scoped);
                        services.Remove(serviceDescriptor);
                        services.AddScoped<IMyActivityService>(s => new MyActivityService(mockHttpClient));
                    });
                });

            var client = application.CreateClient();

            var responseMessage = await client.GetAsync("/MyActivity");

            Assert.AreEqual(HttpStatusCode.NoContent, responseMessage.StatusCode);
        }

        private static HttpClient GetMockedHttpClient(HttpResponseMessage httpResponseMessage)
        {
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            // Set up the SendAsync method behavior.
            httpMessageHandlerMock
                .Protected() // <= this is most important part that it need to setup.
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            // create the HttpClient
            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost") // It should be in valid uri format.
            };

            return httpClient;
        }
    }
}