using System;
using System.Collections.Concurrent;
using System.Threading;

namespace KeyedLock
{
    public class HashedKeyedLocker<TKey, THashedKey>
    {
        private readonly ConcurrentDictionary<THashedKey, SemaphoreSlim> _dictionary;
        private readonly Func<TKey, THashedKey> _hash;
        private readonly int _maxReaders;

        public SemaphoreSlim this[TKey key] => _dictionary.GetOrAdd(_hash(key), _ => new SemaphoreSlim(1,_maxReaders));
        
        public HashedKeyedLocker(Func<TKey, THashedKey> hash) : this (hash, 1)
        {
        }
        
        public HashedKeyedLocker(Func<TKey, THashedKey> hash, int maxReaders)
        {
            _hash = hash;
            _maxReaders = maxReaders;
            _dictionary = new ConcurrentDictionary<THashedKey, SemaphoreSlim>();
        }
    }
}
