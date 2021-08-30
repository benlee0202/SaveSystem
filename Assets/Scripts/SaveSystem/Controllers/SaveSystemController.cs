using EventHandler.Events;
using EventHandler.Interfaces;
using SaveSystem.Interfaces;
using SaveSystem.UseCases;
using SaveSystem.UseCases.UseCaseInputs;

namespace SaveSystem.Controllers
{
    public class SaveSystemController : Controller
    {
        private readonly IFactory<IUseCaseInputBoundary> _saverFactory;
        private readonly ISerializableMethod _serializableMethod;
        private readonly int _dummyID;
        
        public SaveSystemController(int dummyID, IFactory<IUseCaseInputBoundary> saverFactory, 
            ISerializableMethod serializableMethod, IEventBus eventBus) : base(eventBus)
        {
            _dummyID = dummyID;
            _saverFactory = saverFactory;
            _serializableMethod = serializableMethod;
        }

        public override void Enable()
        {
            EventBus.Subscribe<OnSaveData>(CallSaveDataUseCase);
            EventBus.Subscribe<OnUpdateData>(CallUpdateDataUseCase);
            EventBus.Subscribe<OnLoadData>(CallLoadDataUseCase);
        }

        public override void Disable()
        {
            EventBus.Unsubscribe<OnSaveData>(CallSaveDataUseCase);
            EventBus.Unsubscribe<OnUpdateData>(CallUpdateDataUseCase);
            EventBus.Unsubscribe<OnLoadData>(CallLoadDataUseCase);
        }

        private void CallSaveDataUseCase(OnSaveData gameEvent)
        {
            InputBoundary = _saverFactory.GetProduct(typeof(SaveDataUseCase));
            InputBoundary.Execute(new SaveDataUseCaseInput(_dummyID, gameEvent.SaveFileNumber, _serializableMethod));
        }

        private void CallUpdateDataUseCase(OnUpdateData gameEvent)
        {
            InputBoundary = _saverFactory.GetProduct(typeof(UpdateDataUseCase));
            InputBoundary.Execute(new UpdateDataUseCaseInput(gameEvent.SavedView.ViewID, gameEvent.SavedView));
        }

        private void CallLoadDataUseCase(OnLoadData gameEvent)
        {   
            InputBoundary = _saverFactory.GetProduct(typeof(LoadDataUseCase));
            InputBoundary.Execute(new LoadDataUseCaseInput(_dummyID, gameEvent.SaveFileNumber, _serializableMethod));
        }
    }
}
