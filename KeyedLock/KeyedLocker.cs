using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace KeyedLock
{
    public class KeyedLocker<TKey> : HashedKeyedLocker<TKey, TKey>
    {
        public KeyedLocker() : this(1){}
        public KeyedLocker(int maxReaders) : base(x => x, maxReaders){}
    }
}
