using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SaveSystem.Interfaces;

namespace SaveSystem.Utilities
{
    public class SaveByBinaryFormatter : ISerializableMethod
    {
        private readonly string _destination;
        private readonly string _fileName;
    
        public SaveByBinaryFormatter(string destination, string fileName)
        {
            _destination = destination;
            _fileName = fileName;

            if (!Directory.Exists(_destination))
            {
                Directory.CreateDirectory(_destination);
            }
        }
    
        public void Save(int serialNumber, object saveData)
        {
            var formatter = new BinaryFormatter();
            var path = GetPath($"{_fileName}{serialNumber}");
            var file = File.Create(path);
            
            formatter.Serialize(file, saveData);
            file.Close();
        }

        public object Load(int serialNumber)
        {
            var path = GetPath($"{_fileName}{serialNumber}");
            if (!File.Exists(path)) throw new ArgumentException();
        
            var formatter = new BinaryFormatter();
            var file = File.Open(path, FileMode.Open);

            try
            {
                var data = formatter.Deserialize(file);
                file.Close();
                return data;
            }
            catch (Exception)
            {
                file.Close();
                throw;
            }
        }

        private string GetPath(string saveName)
        {
            return _destination + saveName + ".save";
        }
    }
}
