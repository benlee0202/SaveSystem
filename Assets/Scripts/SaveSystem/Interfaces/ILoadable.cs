using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SaveSystem.Interfaces
{
    public interface ILoadable
    {
        void DeserializeSavedData(List<ISerializable> serializableObjects);
    }
}
