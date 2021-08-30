using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using SaveSystem.Entities;
using SaveSystem.Presenters;
using SaveSystem.Repositories;
using SaveSystem.UseCases.UseCaseInputs;

namespace SaveSystem.UseCases
{
    public class SaveDataUseCase : UseCase<SavedCell>
    {
        public SaveDataUseCase(IRepository<SavedCell> entityRepository, IPresenterInputBoundary presenter) : base(entityRepository, presenter)
        {
        }
        
        public override void Execute(IUseCaseInput input)
        { 
            if (!(input is SaveDataUseCaseInput useCaseInput)) throw new ArgumentException();

            var savers = EntityRepository.GetAllItems();
            var serializableSavers =
                savers.Select(saver => new SerializableSaver(saver.EntityID, saver.SaveData)).ToList();
            var serializableMethod = useCaseInput.SerializableMethod;
            serializableMethod.Save(useCaseInput.SaveFileNumber, serializableSavers);
        }
    }
    
    [Serializable]
    public class SerializableSaver
    {
        public SerializableSaver(int entityID, List<ISerializable> saveData)
        {
            EntityID = entityID;
            SaveData = saveData;
        }

        public int EntityID { get; }
        public List<ISerializable> SaveData { get; }
    }
}
