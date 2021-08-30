using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using EventHandler;
using EventHandler.Events;
using NUnit.Framework;
using SaveSystem.Controllers;
using SaveSystem.Entities;
using SaveSystem.Interfaces;
using SaveSystem.Presenters;
using SaveSystem.Repositories;
using SaveSystem.Utilities;
using SaveSystem.Views;
using UnityEngine;

namespace Tests
{
    public class SaveSystemTest
    {
        [SetUp]
        public void SetUp()
        {
            var filePath = $"{Application.persistentDataPath}/saves/test0.save";
            
            if (File.Exists(filePath)) File.Delete(filePath);
        }
        
        [Test]
        public void Update_Data_With_Single_SavedView()
        {
            // Arrange
            var eventBus = new EventBus();
            var savedCellRepository = new SavedCellRepository();

            var controller = new SaveSystemController(0,
                new UseCaseFactory<SavedCell>(savedCellRepository, new SaveSystemPresenter(new SavedViewRepository())),
                new SaveByBinaryFormatter(Application.persistentDataPath + "/saves/", "test"), eventBus);
            var savedView = new FakeSavedView(0, new FakeSavedData());
            
            // Act
            controller.Enable();
            savedView.SavedData.UpdateData("testInt", 10);
            eventBus.Publish(new OnUpdateData(savedView));
            controller.Disable();

            // Assert
            var savedCell = savedCellRepository.GetItem(savedView.ViewID);
            var ints = (Dictionary<string, int>) savedCell.SaveData[0];
            Assert.AreEqual(10, ints["testInt"]);
        }
        
        [Test]
        public void Update_Data_Multiple_Times_With_Single_SavedView()
        {
            // Arrange
            var eventBus = new EventBus();
            var savedCellRepository = new SavedCellRepository();

            var controller = new SaveSystemController(0,
                new UseCaseFactory<SavedCell>(savedCellRepository, new SaveSystemPresenter(new SavedViewRepository())),
                new SaveByBinaryFormatter(Application.persistentDataPath + "/saves/", "test"), eventBus);
            var savedView = new FakeSavedView(0, new FakeSavedData());
            
            // Act
            controller.Enable();
            savedView.SavedData.UpdateData("testInt", 10);
            savedView.SavedData.UpdateData("testInt", 11);
            eventBus.Publish(new OnUpdateData(savedView));
            controller.Disable();

            // Assert
            var savedCell = savedCellRepository.GetItem(savedView.ViewID);
            var ints = (Dictionary<string, int>) savedCell.SaveData[0];
            Assert.AreEqual(11, ints["testInt"]);
        }

        [Test]
        public void Update_Data_With_Multiple_SavedViews()
        {
            // Arrange
            var eventBus = new EventBus();
            var savedCellRepository = new SavedCellRepository();

            var controller = new SaveSystemController(0,
                new UseCaseFactory<SavedCell>(savedCellRepository, new SaveSystemPresenter(new SavedViewRepository())),
                new SaveByBinaryFormatter(Application.persistentDataPath + "/saves/", "test"), eventBus);
            var savedView1 = new FakeSavedView(0, new FakeSavedData());
            var savedView2 = new FakeSavedView(1, new FakeSavedData());
            
            // Act
            controller.Enable();
            savedView1.SavedData.UpdateData("testInt1", 10);
            savedView2.SavedData.UpdateData("testInt2", 11);
            eventBus.Publish(new OnUpdateData(savedView1));
            eventBus.Publish(new OnUpdateData(savedView2));
            controller.Disable();

            // Assert
            var savedCell1 = savedCellRepository.GetItem(savedView1.ViewID);
            var savedCell2 = savedCellRepository.GetItem(savedView2.ViewID);
            var ints1 = (Dictionary<string, int>) savedCell1.SaveData[0];
            var ints2 = (Dictionary<string, int>) savedCell2.SaveData[0];
            Assert.AreEqual(10, ints1["testInt1"]);
            Assert.AreEqual(11, ints2["testInt2"]);
        }

        [Test]
        public void Save_Data()
        {
            // Arrange
            var eventBus = new EventBus();
            var savedCellRepository = new SavedCellRepository();

            var controller = new SaveSystemController(0,
                new UseCaseFactory<SavedCell>(savedCellRepository, new SaveSystemPresenter(new SavedViewRepository())),
                new SaveByBinaryFormatter(Application.persistentDataPath + "/saves/", "test"), eventBus);
            var savedView = new FakeSavedView(0, new FakeSavedData());
            
            // Act
            controller.Enable();
            savedView.SavedData.UpdateData("testInt", 10);
            eventBus.Publish(new OnUpdateData(savedView));
            eventBus.Publish(new OnSaveData(0));
            controller.Disable();
            
            // Assert
            Assert.IsTrue(File.Exists($"{Application.persistentDataPath}/saves/test0.save"));
        }

        [Test]
        public void Load_Data_With_Single_SavedView()
        {
            // Arrange
            var eventBus = new EventBus();
            var savedCellRepository = new SavedCellRepository();
            var savedViewRepository = new SavedViewRepository();
            var presenter = new SaveSystemPresenter(savedViewRepository);

            var controller = new SaveSystemController(0,
                new UseCaseFactory<SavedCell>(savedCellRepository, presenter),
                new SaveByBinaryFormatter
                    (Application.persistentDataPath + "/saves/", "test"), eventBus);
            var savedView = new FakeSavedView(0, new FakeSavedData());
            
            // Act
            controller.Enable();
            savedView.SavedData.UpdateData("testInt", 10);
            eventBus.Publish(new OnUpdateData(savedView));
            eventBus.Publish(new OnSaveData(0));
            savedView.SavedData.UpdateData("testInt", 11);
            eventBus.Publish(new OnLoadData(0));
            controller.Disable();
            
            // Assert
            savedView.SavedData.LoadData("testInt", out int loadedInt);
            Assert.AreEqual(10, loadedInt);            
        }
        
        [Test]
        public void Load_Data_With_Multiple_SavedViews()
        {
            // Arrange
            var eventBus = new EventBus();
            var savedCellRepository = new SavedCellRepository();
            var savedViewRepository = new SavedViewRepository();
            var presenter = new SaveSystemPresenter(savedViewRepository);

            var controller = new SaveSystemController(0,
                new UseCaseFactory<SavedCell>(savedCellRepository, presenter),
                new SaveByBinaryFormatter
                    (Application.persistentDataPath + "/saves/", "test"), eventBus);
            var savedView1 = new FakeSavedView(0, new FakeSavedData());
            var savedView2 = new FakeSavedView(1, new FakeSavedData());
            
            // Act
            controller.Enable();
            savedView1.SavedData.UpdateData("testInt1", 10);
            savedView2.SavedData.UpdateData("testInt2", 20);
            eventBus.Publish(new OnUpdateData(savedView1));
            eventBus.Publish(new OnUpdateData(savedView2));
            eventBus.Publish(new OnSaveData(0));
            savedView1.SavedData.UpdateData("testInt1", 11);
            savedView2.SavedData.UpdateData("testInt2", 21);
            eventBus.Publish(new OnLoadData(0));
            controller.Disable();
            
            // Assert
            savedView1.SavedData.LoadData("testInt1", out int loadedInt1);
            savedView2.SavedData.LoadData("testInt2", out int loadedInt2);
            Assert.AreEqual(10, loadedInt1);
            Assert.AreEqual(20, loadedInt2);
        }
    }

    internal class FakeSavedView : ISavedView
    {
        public FakeSavedView(int viewID, ISavedData savedData)
        {
            ViewID = viewID;
            SavedData = savedData;
        }

        public int ViewID { get; }
        public ISavedData SavedData { get; }
    }
    
    internal class FakeSavedData : ISavedData
    {
        private Dictionary<string, int> _ints = new Dictionary<string, int>();
        public List<ISerializable> GetSerializableObjects()
        {
            return new List<ISerializable> {_ints};
        }

        public void DeserializeSavedData(List<ISerializable> serializableObjects)
        {
            var ints = (Dictionary<string, int>) serializableObjects[0];
            _ints = ints;
        }

        public void UpdateData(string name, int savedInt)
        {
            if (_ints.ContainsKey(name)) _ints[name] = savedInt;
            else _ints.Add(name, savedInt);
        }

        public void UpdateData(string name, bool savedBool)
        {
        }

        public void UpdateData(string name, Vector3 savedVector3)
        {
        }

        public void LoadData(string name, out int loadedInt)
        {
            loadedInt = _ints[name];
        }

        public void LoadData(string name, out bool loadedBool)
        {
            loadedBool = true;
        }

        public void LoadData(string name, out Vector3 loadedVector3)
        {
            loadedVector3 = Vector3.one;
        }
    }
}
