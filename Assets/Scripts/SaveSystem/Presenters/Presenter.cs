using SaveSystem.Presenters.PresenterInputs;

namespace SaveSystem.Presenters
{
    public abstract class Presenter : IPresenterInputBoundary
    {
        public abstract void Execute<TInput>(TInput input) where TInput : IPresenterInput;
    }
}