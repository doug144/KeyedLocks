using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace KeyedLock
{
    public class KeyedLock<TKey> : HashedKeyedLock<TKey, TKey>
    {
        public KeyedLock() : this(1){}
        public KeyedLock(int maxReaders) : base(x => x, maxReaders){}
    }
}
