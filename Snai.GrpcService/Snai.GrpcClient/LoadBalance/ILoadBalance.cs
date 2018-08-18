using Snai.GrpcClient.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snai.GrpcClient.LoadBalance
{
    /*
     * 负载均衡接口
     */
    public interface ILoadBalance
    {
        string GetGrpcService(string ServiceName);
    }
}
