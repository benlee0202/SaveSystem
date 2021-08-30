using System;
using System.Collections.Generic;
using System.Linq;
using SaveSystem.Entities;
using SaveSystem.Presenters;
using SaveSystem.Presenters.PresenterInputs;
using SaveSystem.Repositories;
using SaveSystem.UseCases.UseCaseInputs;

namespace SaveSystem.UseCases
{
    public class LoadDataUseCase : UseCase<SavedCell>
    {
        public LoadDataUseCase(IRepository<SavedCell> entityRepository, IPresenterInputBoundary presenter) : base(entityRepository, presenter)
        {
        }

        public override void Execute(IUseCaseInput input)
        {
            if (!(input is LoadDataUseCaseInput useCaseInput)) throw new ArgumentException();
            
            var serializableMethod = useCaseInput.SerializableMethod;
            var savers = (List<SerializableSaver>) serializableMethod.Load(useCaseInput.LoadFileNumber);
            var serializableObjectMap = 
                savers.ToDictionary(saver => saver.EntityID, saver => saver.SaveData);
            Presenter.Execute(new LoadDataPresenterInput(useCaseInput.EntityID, serializableObjectMap));
        }
    }
}
