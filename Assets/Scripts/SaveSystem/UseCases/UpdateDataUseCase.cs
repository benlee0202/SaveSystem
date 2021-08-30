using System;
using SaveSystem.Entities;
using SaveSystem.Presenters;
using SaveSystem.Presenters.PresenterInputs;
using SaveSystem.Repositories;
using SaveSystem.UseCases.UseCaseInputs;

namespace SaveSystem.UseCases
{
    public class UpdateDataUseCase : UseCase<SavedCell>
    {
        public UpdateDataUseCase(IRepository<SavedCell> entityRepository, IPresenterInputBoundary presenter) : base(entityRepository, presenter)
        {
        }

        public override void Execute(IUseCaseInput input)
        {
            if (!(input is UpdateDataUseCaseInput useCaseInput)) throw new ArgumentException();

            var saver = EntityRepository.GetItem(useCaseInput.EntityID);
            saver.SaveData = useCaseInput.SavedView.SavedData.GetSerializableObjects();
            Presenter.Execute(new UpdateDataPresenterInput(useCaseInput.EntityID, useCaseInput.SavedView));
        }
    }
}
