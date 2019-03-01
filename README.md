# KeyedLocks
Allows using [SemaphoreSlim](https://docs.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim?view=netstandard-2.0)
locks by a given key of any type. For example:

````csharp
public class Foo
{
    private KeyedLocker<string> _locker = new KeyedLock<string>();
  
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
