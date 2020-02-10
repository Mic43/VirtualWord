namespace VirtualWord.Utils
{
    public interface IObserver<in T> where T:class
    {
        void Handle(T value);
    }
}