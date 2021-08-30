using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SaveSystem.Entities
{
    public class SavedCell : Entity
    {
        public List<ISerializable> SaveData { get; set; }
        public SavedCell(int entityID) : base(entityID)
        {
        }
    }
}
