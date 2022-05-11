using System.Collections.Generic;

namespace RpnApi.Services
{
    public class OperandService : IOperandService
    {
        private readonly IList<string> operants;

        public OperandService()
        {
            operants = new List<string>() { "+", "-", "*", "/" };
        }
        public IEnumerable<string> Get()
        {
            return operants;
        }
    }
}
