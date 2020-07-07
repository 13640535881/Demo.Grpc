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
            ////创建交换机
            //using var channel = GrpcChannel.ForAddress("https://localhost:5020");
            /*
             * 常见模式使用 HttpClient 连接到具有不应验证的证书的服务器，例如自签名证书。 
             * 通常使用 HttpClientHandler，方法是将 ServerCertificateCustomValidationCallback 属性设置为始终返回 True的委托;这表明证书已通过验证。
             * 开发人员可以使用此属性使工具更轻松地标记禁用证书验证的风险，使开发人员可以更轻松地避免发送不安全的应用程序。
            */
            var channel = GrpcChannel.ForAddress("http://localhost:5020", new GrpcChannelOptions
            {
                HttpClient = new HttpClient(new HttpClientHandler() { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator })
            });
            //使用交换机生成客户端
            var client = new Greeter.GreeterClient(channel);
            //客户端调用服务端的方法并返回结果
            var response = client.SayHello(new HelloRequest { Name = "gRPCWeb 测试" });
            ViewBag.name = response.Message;
        }
    }
}