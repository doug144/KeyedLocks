using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace KeyedLock
{
    /// <summary>
    /// A locker that holds a lock in memory for each given key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class KeyedLocker<TKey> : HashedKeyedLocker<TKey, TKey>
    {
        /// <summary>
        /// Constructor that defaults to 1 maximum reader per lock
        /// </summary>
        /// <returns></returns>
        public KeyedLocker() : this(1) { }

        /// <summary>
        /// </summary>
        /// <param name="maxReaders">The maximum number of readers for a given lock</param>
        /// <returns></returns>
        public KeyedLocker(int maxReaders) : base(x => x, maxReaders) { }
    }
}
