using SaveSystem.Interfaces;

namespace SaveSystem.UseCases.UseCaseInputs
{
    public readonly struct SaveDataUseCaseInput : IUseCaseInput
    {
        public SaveDataUseCaseInput(int entityID, int saveFileNumber, ISerializableMethod serializableMethod)
        {
            EntityID = entityID;
            SaveFileNumber = saveFileNumber;
            SerializableMethod = serializableMethod;
        }

        public int EntityID { get; }
        public int SaveFileNumber { get; }
        public ISerializableMethod SerializableMethod { get; }
        
    }
}
