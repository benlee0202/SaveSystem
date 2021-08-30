using SaveSystem.Views;

namespace SaveSystem.UseCases.UseCaseInputs
{
    public readonly struct UpdateDataUseCaseInput : IUseCaseInput
    {
        public UpdateDataUseCaseInput(int entityID, ISavedView savedView)
        {
            EntityID = entityID;
            SavedView = savedView;
        }

        public int EntityID { get; }
        public ISavedView SavedView { get; }
    }
}
