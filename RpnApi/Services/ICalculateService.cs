using System.Collections.Generic;
using RpnApi.Models;

namespace RpnApi.Services
{
    public interface ICalculateService
    {
        Stack<Entry> Calculate(int id, string operand);
    }
}
