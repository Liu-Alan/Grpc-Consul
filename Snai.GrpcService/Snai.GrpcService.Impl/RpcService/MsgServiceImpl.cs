using Grpc.Core;
using Snai.GrpcService.Protocol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Snai.GrpcService.Impl.RpcService
{
    public class MsgServiceImpl : MsgService.MsgServiceBase
    {
        public override async Task<GetMsgSumReply> GetSum(GetMsgNumRequest request, ServerCallContext context)
        {
            var result = new GetMsgSumReply();

            result.Sum = request.Num1 + request.Num2;

            Console.WriteLine(request.Num1 + "+" + request.Num2 + "=" + result.Sum);

            return result;
        }
    }
}
