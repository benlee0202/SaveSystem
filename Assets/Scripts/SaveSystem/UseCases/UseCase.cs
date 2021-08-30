using SaveSystem.Entities;
using SaveSystem.Presenters;
using SaveSystem.Repositories;
using SaveSystem.UseCases.UseCaseInputs;

namespace SaveSystem.UseCases
{
    public abstract class UseCase<TEntity> : IUseCaseInputBoundary where TEntity : Entity
    {
        protected readonly IRepository<TEntity> EntityRepository;
        protected readonly IPresenterInputBoundary Presenter;

        protected UseCase(IRepository<TEntity> entityRepository, IPresenterInputBoundary presenter)
        {
            EntityRepository = entityRepository;
            Presenter = presenter;
        }
        public abstract void Execute(IUseCaseInput input);
    }
}