using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace KeyedLock.UnitTests
{
    [TestClass]
    public class KeyedLockTests
    {
        [TestMethod]
        public void GetLockByString_SameKey_SameLock()
        {
            //arrange
            var locker = new KeyedLocker<string>();

            //act
            var sem = locker["test"];
            var sem2 = locker["test"];

            //assert
            sem.Should().Be(sem2);
        }

        [TestMethod]
        public void GetLockByString_DifferentKey_DifferentLock()
        {
            //arrange
            var locker = new KeyedLocker<string>();

            //act
            var sem = locker["test"];
            var sem2 = locker["other test"];

            //assert
            sem.Should().NotBe(sem2);
        }
    }
}
