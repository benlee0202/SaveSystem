using System;
using System.Collections.Generic;
using SaveSystem.Presenters.PresenterInputs;
using SaveSystem.Repositories;
using SaveSystem.Views;

namespace SaveSystem.Presenters
{
    public class SaveSystemPresenter : Presenter
    {
        private readonly Dictionary<Type, Action<IPresenterInput>> _actionMap;
        private readonly IRepository<ISavedView> _viewMap;
        
        public SaveSystemPresenter(IRepository<ISavedView> viewMap)
        {
            _viewMap = viewMap;
            _actionMap = new Dictionary<Type, Action<IPresenterInput>>
            {
                {typeof(UpdateDataPresenterInput), input => UpdateData((UpdateDataPresenterInput) input)},
                {typeof(LoadDataPresenterInput), input => LoadData((LoadDataPresenterInput) input)}
            };
        }

        public override void Execute<TInput>(TInput input)
        {
            if (_actionMap.TryGetValue(input.GetType(), out var action)) action.Invoke(input);
        }
        
        private void UpdateData(UpdateDataPresenterInput input)
        {
            _viewMap.Add(input.SavedView);
        }
        
        private void LoadData(LoadDataPresenterInput input)
        {
            var allViews = _viewMap.GetAllItems();
            foreach (var view in allViews)
            {
                view.SavedData.DeserializeSavedData(input.SerializableObjectMap[view.ViewID]);
            }
        }
    }
}
