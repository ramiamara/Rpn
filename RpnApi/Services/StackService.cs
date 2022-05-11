using System.Collections.Generic;
using RpnApi.Models;

namespace RpnApi.Services
{
    public class StackService : IStackService
    {
        private readonly Dictionary<int, Stack<Entry>> items;

        public StackService()
        {
            items = new Dictionary<int,Stack<Entry>>();
        }

        public IDictionary<int, Stack<Entry>> Get()
        {
            return items;
        }

        public Stack<Entry> Get(int id)
        {
            return items.ContainsKey(id) ? items[id] : null;
        }

        public Stack<Entry> Add(IEnumerable<Entry> entries)
        {   
            var key = items.Count;
            var stack =new Stack<Entry>();
            foreach (var entry in entries)
            {
                stack.Push(entry);
            }
            items.Add(key, stack);
            return stack;
        }

        public Stack<Entry> AddEntry(int id, Entry entry)
        {
            if (items.ContainsKey(id))
            {
                items[id].Push(entry);
                return items[id];
            }
            return null;
        }

        public bool Delete(int id)
        {
            if (items.ContainsKey(id))
            {
                items[id].Clear();
                return true;
            }
            return false;
        }
    }
}
