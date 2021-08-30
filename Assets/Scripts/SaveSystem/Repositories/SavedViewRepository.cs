using System.Collections.Generic;
using System.Linq;
using SaveSystem.Views;

namespace SaveSystem.Repositories
{
    public class SavedViewRepository : IRepository<ISavedView>
    {
        private readonly Dictionary<int, ISavedView> _saverViewMap = new Dictionary<int, ISavedView>();

        public void Add(ISavedView item)
        {
            if (_saverViewMap.ContainsKey(item.ViewID))
            {
                _saverViewMap[item.ViewID] = item;
            }
            else
            {
                _saverViewMap.Add(item.ViewID, item);
            }
        }

        public void Remove(ISavedView item)
        {
            throw new System.NotImplementedException();
        }

        public ISavedView GetItem(int serialNumber)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(ISavedView item)
        {
            throw new System.NotImplementedException();
        }

        public List<ISavedView> GetAllItems()
        {
            return _saverViewMap.Select(saverView => saverView.Value).ToList();
        }
    }
}
