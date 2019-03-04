using System;
using System.Collections.Concurrent;
using System.Threading;

namespace KeyedLock
{
    /// <summary>
    /// Locker that hashes the keys to minimize the nubmer of locks held in memory
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="THashedKey"></typeparam>
    public class HashedKeyedLocker<TKey, THashedKey>
    {
        private readonly ConcurrentDictionary<THashedKey, SemaphoreSlim> _dictionary;
        private readonly Func<TKey, THashedKey> _hash;
        private readonly int _maxReaders;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SemaphoreSlim this[TKey key] => _dictionary.GetOrAdd(_hash(key), _ => new SemaphoreSlim(1,_maxReaders));
        
        /// <summary>
        /// Constructor that defaults to 1 maximum reader per lock
        /// </summary>
        /// <param name="hash">Hash function for the key used to decide which lock will be returned</param>
        /// <returns></returns>
        public HashedKeyedLocker(Func<TKey, THashedKey> hash) : this (hash, 1)
        {
        }
        
        /// <summary>
        /// </summary>
        /// <param name="hash">Hash function for the key used to decide which lock will be returned</param>
        /// <param name="maxReaders">The maximum number of readers for a given lock</param>
        public HashedKeyedLocker(Func<TKey, THashedKey> hash, int maxReaders)
        {
            _hash = hash;
            _maxReaders = maxReaders;
            _dictionary = new ConcurrentDictionary<THashedKey, SemaphoreSlim>();
        }
    }
}
