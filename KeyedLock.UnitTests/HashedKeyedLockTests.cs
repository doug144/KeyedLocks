using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Collections.Generic;

namespace KeyedLock.UnitTests
{
    [TestClass]
    public class HashedKeyedLockTests
    {
        private Dictionary<string, int> _dict;
        private HashedKeyedLocker<string, int> _locker;

        [TestInitialize]
        public void TestInitialize()
        {
            _dict = new Dictionary<string, int>();
            _locker = new HashedKeyedLocker<string, int>(x => _dict.GetValueOrDefault(x));
        }

        [TestMethod]
        public void GetLockByString_SameKey_SameLock()
        {
            //arrange
            const string key = "test";
            _dict[key] = 1;

            //act
            var sem = _locker[key];
            var sem2 = _locker[key];

            //assert
            sem.Should().Be(sem2);
        }

        [TestMethod]
        public void GetLockByString_DifferentKey_DifferentLock()
        {
            //arrange
            const string key = "test";
            const string key2 = "other test";
            _dict[key] = 1;
            _dict[key2] = 2;

            //act
            var sem = _locker[key];
            var sem2 = _locker[key2];

            //assert
            sem.Should().NotBe(sem2);
        }

        [TestMethod]
        public void GetLockByString_DifferentKeySameHash_DifferentLock()
        {
            //arrange
            const string key = "test";
            const string key2 = "other test";
            _dict[key] = 1;
            _dict[key2] = 1;

            //act
            var sem = _locker[key];
            var sem2 = _locker[key2];

            //assert
            sem.Should().Be(sem2);
        }
    }
}
