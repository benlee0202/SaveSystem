SaveSystem is a tool that saves and loads data during gameplay.
## Architecture
SaveSystem is applied by Clean Architecture, which is written by Robert C. Martin. The core concepts as follow.
* Decoupling business rules and user interfaces
* An inner circle (entities, policies) which knows nothing about an outer circle (user interfaces, Devices)

The benefit of a clean architecture is that the policies are entirely independent, and any classes which implement `ISavedView` can save data. To achieve this architecture, there are some layers that must be implemented.
*	Controller: an adaptor which is used to transfer outer information to a use case input. In SaveSystem, `SaveSystemController` get the information by subscribing `IEventBus`.
*	UseCase: this layer applies the input from a controller to manipulate entities and output the data given from entities to a presenter.
*	Entity: in this case, the entity is a save-capable cell, `List<ISerializable>`. Note that a save-capable cell knows nothing about outer information.
*	Presenter: another adaptor which transfers data from an entity to a`ISavedView`. In `SaveSystemPresenter`, the presenter stores a `ISavedView` which is updated and send the serializable data to each `ISavedView` to deserialize.
## Usage
There are three events which drive the `SaveSystemController`.
*	`OnUpdateData`: publishing this event with argument `ISavedView` can update data in the related save-capable cell. Note that the data hasn’t been exported to a file in this phase yet.
*	`OnSaveData`: publish this event when you want to export data as a file. Since `SaveDataUseCase` only knows the interface, `ISerializableMethod`, you can replace `ISerializableMethod` with another save and load method. In this case, we use the binary formatter.
*	`OnLoadData`: publishing this event when loading data from the file. `SaveSystemPresenter` restores data of all `ISavedView`, and deserializing the serializable data that depends on each class which implements `ISavedData`.
Here is an example of how to use SaveSystem in Unity.

```c#
public class SavedView : MonoBehaviour, ISavedView
{
    public int ViewID => GetInstanceID();
    public ISavedData SavedData { get; private set; }
    
    private Vector3 _position;
    private IEventBus _eventBus;

    private void Start()
    {
        SavedData = new SavedDataByDictionary();
        _ eventBus = new EventBus();
        
        _position = Vector3.zero;
        SavedData.UpdateData("PlayerPosition", _position);
        _eventBus.Publish(new OnUpdateData(this));
        
        _eventBus.Publish(new OnSaveData(0));

        _position = Vector3.one;
        SavedData.UpdateData("PlayerPosition", _position);
        _eventBus.Publish(new OnUpdateData(this));
        
        _eventBus.Publish(new OnLoadData(0));
        SavedData.LoadData("PlayerPosition", out _position);
        
        // _position will be restored to Vector3.zero
    }
}
```
