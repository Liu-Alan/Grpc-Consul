using System;
using System.Collections.Generic;
using System.Text;

namespace Snai.GrpcClient.Consul
{
    public interface IAppFind
    {
        IEnumerable<string> FindConsul(string ServiceName);
    }
}
