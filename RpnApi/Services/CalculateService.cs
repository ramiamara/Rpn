using System;
using System.Collections.Generic;
using RpnApi.Models;

namespace RpnApi.Services
{
    public class CalculateService : ICalculateService
    {
        private readonly IStackService _stackService;
        public CalculateService(IStackService stackService)
        {
            _stackService = stackService;
        }
        public Stack<Entry> Calculate(int id, string operand)
        {
            var stack = _stackService.Get(id);

            if (stack == null) { return null; }
            if (stack.Count < 2) { throw new InvalidOperationException("Invalid input"); }

            int x = stack.Pop().Value;
            int y = stack.Pop().Value;
            try
            {
                return Compute(x, y, stack, operand);
            }
            catch (DivideByZeroException e)
            {
                stack.Push(new Entry(y));
                stack.Push(new Entry(x));
                throw new DivideByZeroException(e.Message);
            }
            catch (Exception e)
            {
                stack.Push(new Entry(y));
                stack.Push(new Entry(x));
                throw new Exception(e.Message);
            }
        }

        private static Stack<Entry> Compute(int x, int y, Stack<Entry> stack, string operand)
        {
            if (operand == "+") x += y;
            else if (operand == "-") x -= y;
            else if (operand == "*") x *= y;
            else if (operand.Replace("%2F", "/") == "/") x /= y;
            else throw new Exception();

            stack.Push(new Entry(x));
            return stack;
        }
    }
}
