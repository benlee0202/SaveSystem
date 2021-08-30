using SaveSystem.Presenters.PresenterInputs;

namespace SaveSystem.Presenters
{
    public interface IPresenterInputBoundary
    {
        void Execute<T>(T input) where T : IPresenterInput;
    }
}