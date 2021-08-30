using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SaveSystem.Interfaces;
using UnityEngine;

namespace SaveSystem.SaveData
{
    public class SavedDataByDictionary : ISavedData
    {
        private Dictionary<string, int> _intMap = new Dictionary<string, int>();
        private Dictionary<string, bool> _boolMap = new Dictionary<string, bool>();
        private Dictionary<string, List<float>> _vector3Map = new Dictionary<string, List<float>>();

        public void UpdateData(string name, int savedInt)
        {
            if (_intMap.ContainsKey(name)) _intMap[name] = savedInt;
            else _intMap.Add(name, savedInt);
        }
    
        public void UpdateData(string name, bool savedBool)
        {
            if (_boolMap.ContainsKey(name)) _boolMap[name] = savedBool;
            else _boolMap.Add(name, savedBool);
        }
    
        public void UpdateData(string name, Vector3 savedVector3)
        {
            if (_vector3Map.ContainsKey(name))
            {
                _vector3Map[name][0] = savedVector3.x;
                _vector3Map[name][1] = savedVector3.y;
                _vector3Map[name][2] = savedVector3.z;
            }
            else
            {
                _vector3Map.Add(name, new List<float> {savedVector3.x, savedVector3.y, savedVector3.z});
            }
        }

        public void LoadData(string name, out int loadedInt)
        {
            if (!_intMap.TryGetValue(name, out loadedInt))
            {
                throw new ArgumentException("Name of the variable is not existed");
            }
        }
    
        public void LoadData(string name, out bool loadedBool)
        {
            if (!_boolMap.TryGetValue(name, out loadedBool))
            {
                throw new ArgumentException("Name of the variable is not existed");
            }
        }
    
        public void LoadData(string name, out Vector3 loadedVector3)
        {
            if (!_vector3Map.TryGetValue(name, out var serializableVector3))
            {
                throw new ArgumentException("Name of the variable is not existed");
            }

            loadedVector3 = new Vector3(serializableVector3[0], serializableVector3[1], serializableVector3[2]);
        }
    
        private Dictionary<string, T> DeserializeSaveData<T>(object serializableObject)
        {
            var deSerializableObject = (Dictionary<string, T>) serializableObject;
            if (deSerializableObject != null) return deSerializableObject;
        
            throw new ArgumentException("Type is not matched");
        }

        public List<ISerializable> GetSerializableObjects()
        {
            return new List<ISerializable> {_intMap, _boolMap, _vector3Map};
        }

        public void DeserializeSavedData(List<ISerializable> serializableObjects)
        {
            _intMap = DeserializeSaveData<int>(serializableObjects[0]);
            _boolMap = DeserializeSaveData<bool>(serializableObjects[1]);
            _vector3Map = DeserializeSaveData<List<float>>(serializableObjects[2]);
        }
    }
}
