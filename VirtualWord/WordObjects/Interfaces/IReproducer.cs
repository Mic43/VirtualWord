namespace VirtualWord.WordObjects.Interfaces
{
    public interface IReproducer
    {
        bool CanReproduceNow { get; }
        void SetJustReproduced();
    }
}