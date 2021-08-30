using System.Collections.Generic;
using System.Linq;
using SaveSystem.Entities;

namespace SaveSystem.Repositories
{
    public class SavedCellRepository : IRepository<SavedCell>
    {
        private readonly Dictionary<int, SavedCell> _saverMap = new Dictionary<int, SavedCell>();
        public void Add(SavedCell item)
        {
        }

        public void Remove(SavedCell item)
        {
        }

        public SavedCell GetItem(int serialNumber)
        {
            if (_saverMap.TryGetValue(serialNumber, out var saver)) return saver;
            
            var newSaver = new SavedCell(serialNumber);
            _saverMap.Add(serialNumber, newSaver);
            return _saverMap[serialNumber];
        }

        public void Edit(SavedCell item)
        {
        }

        public List<SavedCell> GetAllItems()
        {
            // Collect items in _saverMap and return.
            return _saverMap.Select(pair => pair.Value).ToList();
        }
    }
}
