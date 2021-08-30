using SaveSystem.Interfaces;

namespace SaveSystem.UseCases.UseCaseInputs
{
    public readonly struct LoadDataUseCaseInput : IUseCaseInput
    {
        public LoadDataUseCaseInput(int entityID, int loadFileNumber, ISerializableMethod serializableMethod)
        {
            EntityID = entityID;
            LoadFileNumber = loadFileNumber;
            SerializableMethod = serializableMethod;
        }

        public int EntityID { get; }
        public int LoadFileNumber { get; }
        public ISerializableMethod SerializableMethod { get; }
    }
}
