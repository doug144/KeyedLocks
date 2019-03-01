# KeyedLocks
Allows using [SemaphoreSlim](https://docs.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim?view=netstandard-2.0)
locks by a given key of any type.

## Usage

````csharp
public class Foo
{
    private KeyedLocker<string> _locker = new KeyedLocker<string>();
  
    public void DoSomethingWithLock(string keyToLock)
    {
        _locker[keyToLock].Wait();
        try
        {
            //do something...
        }
        finally
        {
            _locker[keyToLock].Release();
        }
    }
}
````

If your code will contain many keys, consider using the [HashedKeyedLocker](/KeyedLock/HashedKeyedLocker.cs) with a hash function to minimize the nubmer of 
[SemaphoreSlim](https://docs.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim?view=netstandard-2.0)
objects that will be held in memory.

````csharp
public class Foo
{
    private HashedKeyedLock<long, int> _locker = new HashedKeyedLocker<long, int>(long x => (int)(x % 10)); // limit to 10 locks in memory
  
    public void DoSomethingWithLock(long keyToLock)
    {
        _locker[keyToLock].Wait();
        try
        {
            //do something...
        }
        finally
        {
            _locker[keyToLock].Release();
        }
    }
}
````
## Resources
Some of the ideas for this repo were taken from [this blog post](https://www.tabsoverspaces.com/233703-named-locks-using-monitor-in-net-implementation).
