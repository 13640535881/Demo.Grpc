using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.ClientFactory;
using Grpc.ServiceTest;
using Microsoft.AspNetCore.Mvc;

namespace Grpc.ClientTest.Controlers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            dass();
            return Ok(ViewBag.name);
        }

        private void dass()
        {
            //using var channel = GrpcChannel.ForAddress("https://localhost:5020");
            var channel = GrpcChannel.ForAddress("http://localhost:5020", new GrpcChannelOptions
            {
                HttpClient = new HttpClient(new HttpClientHandler() { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator })
            });
            var client = new Greeter.GreeterClient(channel);
            var response = client.SayHello(new HelloRequest { Name = "gRPCWeb 测试" });
            ViewBag.name = response.Message;
        }
    }
}