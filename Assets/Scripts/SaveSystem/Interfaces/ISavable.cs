using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SaveSystem.Interfaces
{
    public interface ISavable
    {
        List<ISerializable> GetSerializableObjects();
    }
}
