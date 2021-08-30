namespace SaveSystem.Interfaces
{
    public interface ISerializableMethod
    {
        void Save(int serialNumber, object saveData);
        object Load(int serialNumber);
    }
}   