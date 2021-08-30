using UnityEngine;

namespace SaveSystem.Interfaces
{
    public interface ISavedData : ISavable, ILoadable
    {
        void UpdateData(string name, int savedInt);
        void UpdateData(string name, bool savedBool);
        void UpdateData(string name, Vector3 savedVector3);
        void LoadData(string name, out int loadedInt);
        void LoadData(string name, out bool loadedBool);
        void LoadData(string name, out Vector3 loadedVector3);
    }   
}
