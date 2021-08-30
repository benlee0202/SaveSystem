using SaveSystem.UseCases.UseCaseInputs;

namespace SaveSystem.UseCases
{
    public interface IUseCaseInputBoundary
    {
        void Execute(IUseCaseInput input);
    }
}