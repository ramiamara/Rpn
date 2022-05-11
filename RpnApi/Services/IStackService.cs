using System.Collections.Generic;
using RpnApi.Models;

namespace RpnApi.Services
{
    public interface IStackService
    {
        IDictionary<int, Stack<Entry>> Get();
        Stack<Entry> Get(int id);
        Stack<Entry> Add(IEnumerable<Entry> stack);
        Stack<Entry> AddEntry(int id, Entry entry);
        bool Delete(int id);
    }
}
