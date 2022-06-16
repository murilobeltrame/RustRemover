using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Xunit;

namespace RustRemover.Api.Tests.Integration
{
    public class BeerControllerTests
    {
        [Fact]
        public void GetShouldReturnAList()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("Default", "{controller}/");
            var server = new HttpServer(config);
            using (var client = new HttpMessageInvoker(server))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost/api/Beers"))
                {
                    using (var response = client.SendAsync(request, CancellationToken.None).Result)
                    {
                        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    }
                }
            }
        }   
    }
}
