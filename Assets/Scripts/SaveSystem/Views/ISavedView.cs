using SaveSystem.Interfaces;

namespace SaveSystem.Views
{
    public interface ISavedView
    {
        int ViewID { get; }
        ISavedData SavedData { get; }
    }
}
