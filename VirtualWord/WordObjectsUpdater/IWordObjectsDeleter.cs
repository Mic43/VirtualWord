using VirtualWord.WordObjects;

namespace VirtualWord.WordObjectsUpdater
{
    public interface IWordObjectsDeleter
    {
        void Delete(WordObject wordObject);
    }
}