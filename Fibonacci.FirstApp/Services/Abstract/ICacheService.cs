using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci.FirstApp.Services.Abstract
{
    public interface ICacheService<TKey, TValue> where TKey : notnull
    {
        bool TryGetValue(TKey key, out TValue value);
        void Set(TKey key, TValue item);
    }
}
