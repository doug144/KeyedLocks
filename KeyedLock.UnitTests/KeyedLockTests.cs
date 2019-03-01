using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;
using System.Threading.Tasks;

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

        [TestMethod]
        public async Task LockByGuid_SameKey_Waits()
        {
            //arrange
            var locker = new KeyedLocker<Guid>();
            var key = Guid.NewGuid();

            const int shortDelay = 50;
            var str = "";

            Func<Task> lockThenWait = async () =>
            {
                await locker[key].WaitAsync();
                await Task.Delay(shortDelay * 10);

                str += "1";

                locker[key].Release();
            };

            Func<Task> waitThenLock = async () =>
            {
                await Task.Delay(shortDelay);
                await locker[key].WaitAsync();

                str += "2";

                locker[key].Release();
            };

            //act
            var t1 = lockThenWait();
            var t2 = waitThenLock();
            await Task.WhenAll(new[] { t1, t2 });

            //assert
            str.Should().Be("12");
        }

        [TestMethod]
        public async Task LockByGuid_DifferentKey_DoesntWait()
        {
            //arrange
            var locker = new KeyedLocker<Guid>();
            var key1 = Guid.NewGuid();
            var key2 = Guid.NewGuid();

            const int shortDelay = 50;
            var str = "";

            Func<Task> lockThenWait = async () =>
            {
                await locker[key1].WaitAsync();
                await Task.Delay(shortDelay * 10);

                str += "1";

                locker[key1].Release();
            };

            Func<Task> waitThenLock = async () =>
            {
                await Task.Delay(shortDelay);
                await locker[key2].WaitAsync();

                str += "2";

                locker[key2].Release();
            };

            //act
            var t1 = lockThenWait();
            var t2 = waitThenLock();
            await Task.WhenAll(new[] { t1, t2 });

            //assert
            str.Should().Be("21");
        }
    }
}
