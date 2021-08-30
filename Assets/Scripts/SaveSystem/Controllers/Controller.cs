using EventHandler.Interfaces;
using SaveSystem.UseCases;

namespace SaveSystem.Controllers
{
    public abstract class Controller
    {
        protected IUseCaseInputBoundary InputBoundary;
        protected readonly IEventBus EventBus;

        protected Controller(IEventBus eventBus)
        {
            EventBus = eventBus;
        }

        public abstract void Enable();
        public abstract void Disable();
    }
}