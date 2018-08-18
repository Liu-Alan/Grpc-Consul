using Snai.GrpcClient.Consul;
using Snai.GrpcClient.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Snai.GrpcClient.Framework.Entity;
using Microsoft.Extensions.Options;

namespace Snai.GrpcClient.LoadBalance
{
    /*
     * 权重轮询
     */
    public class WeightRoundBalance : ILoadBalance
    {
        int Balance;
        IOptions<GrpcServiceSettings> GrpcSettings;
        IAppFind AppFind;

        public WeightRoundBalance(IOptions<GrpcServiceSettings> grpcSettings, IAppFind appFind)
        {
            Balance = 0;
            GrpcSettings = grpcSettings;
            AppFind = appFind;
        }

        public string GetGrpcService(string ServiceName)
        {
            var grpcServices = GrpcSettings.Value.GrpcServices;

            var healthServiceID = AppFind.FindConsul(ServiceName);

            if (grpcServices == null || grpcServices.Count() == 0 || healthServiceID == null || healthServiceID.Count() == 0)
            {
                return "";
            }

            //健康的服务
            var healthServices = new List<Framework.Entity.GrpcService>();

            foreach (var service in grpcServices)
            {
                foreach (var health in healthServiceID)
                {
                    if (service.ServiceID.Equals(health, StringComparison.CurrentCultureIgnoreCase))
                    {
                        healthServices.Add(service);
                        break;
                    }
                }
            }

            if (healthServices == null || healthServices.Count() == 0)
            {
                return "";
            }

            //加权轮询
            var services = new List<string>();

            foreach (var service in healthServices)
            {
                services.AddRange(Enumerable.Repeat(service.IP + ":" + service.Port, service.Weight));
            }
            
            var servicesArray = services.ToArray();

            Balance = Balance % servicesArray.Length;
            var grpcUrl = servicesArray[Balance];
            Balance = Balance + 1;

            return grpcUrl;
        }
    }
}
