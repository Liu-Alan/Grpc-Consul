using Grpc.Core;
using Microsoft.Extensions.Options;
using Snai.GrpcService.Impl.RpcService;
using Snai.GrpcService.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snai.GrpcService.Impl
{
    public class RpcConfig: IRpcConfig
    {
        private static Server _server;
        static IOptions<Entity.GrpcService> GrpcSettings;

        public RpcConfig(IOptions<Entity.GrpcService> grpcSettings)
        {
            GrpcSettings = grpcSettings;
        }

        public void Start()
        {
            _server = new Server
            {
                Services = { MsgService.BindService(new MsgServiceImpl()) },
                Ports = { new ServerPort(GrpcSettings.Value.IP, GrpcSettings.Value.Port, ServerCredentials.Insecure) }
            };
            _server.Start();

            Console.WriteLine($"Grpc ServerListening On Port {GrpcSettings.Value.Port}");
        }
    }
}
