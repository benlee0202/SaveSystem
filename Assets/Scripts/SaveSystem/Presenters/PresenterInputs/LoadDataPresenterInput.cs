using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SaveSystem.Presenters.PresenterInputs
{
    public readonly struct LoadDataPresenterInput : IPresenterInput
    {
        public LoadDataPresenterInput(int viewInstanceID, Dictionary<int, List<ISerializable>> serializableObjectMap)
        {
            ViewID = viewInstanceID;
            SerializableObjectMap = serializableObjectMap;
        }

        public int ViewID { get; }
        public Dictionary<int, List<ISerializable>> SerializableObjectMap { get; }
    }
}
