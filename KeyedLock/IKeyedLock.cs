using System.Threading.Tasks;

namespace KeyedLock
{
    public interface IKeyedLock<T>
    {
        Task WaitAsync(T key);
        void Wait(T key);
    }
}
