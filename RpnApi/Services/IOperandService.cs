using System.Collections.Generic;

namespace RpnApi.Services
{
    public interface IOperandService
    {
        IEnumerable<string> Get();
    }
}
