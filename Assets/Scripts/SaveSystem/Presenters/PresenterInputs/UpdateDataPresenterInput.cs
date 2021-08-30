using SaveSystem.Views;

namespace SaveSystem.Presenters.PresenterInputs
{
    public readonly struct UpdateDataPresenterInput : IPresenterInput
    {
        public UpdateDataPresenterInput(int viewInstanceID, ISavedView savedView)
        {
            ViewID = viewInstanceID;
            SavedView = savedView;
        }

        public int ViewID { get; }
        public ISavedView SavedView { get; }
    }
}
