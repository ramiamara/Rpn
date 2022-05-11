using System;
using System.Collections.Generic;
using RpnApi.Models;

namespace RpnApi.Services
{
    public class CalculateService : ICalculateService
    {
        private readonly IStackService _stackService;
        public CalculateService(IStackService stackService, IOperandService operandService)
        {
            _stackService = stackService;
        }
        public Stack<Entry> Calculate(int id, string operand)
        {
            var stack = _stackService.Get(id);
            if (stack == null) { return null; }
            if (stack.Count < 2) { return stack; }
            int x = stack.Pop().Value;
            int y = stack.Pop().Value;
            try
            {
                if (operand == "+") x += y;
                else if (operand == "-") x -= y;
                else if (operand == "*") x *= y;
                else if (operand.Replace("%2F", "/") == "/") x /= y;
                else throw new Exception();

                stack.Push(new Entry(x));
                return stack;
            }
            catch (DivideByZeroException e)
            {
                stack.Push(new Entry(y));
                stack.Push(new Entry(x));
                throw new DivideByZeroException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
