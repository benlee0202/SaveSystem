using EventHandler.Interfaces;

namespace EventHandler.Events
{
    public readonly struct OnLoadData : IEventBase
    {
        public OnLoadData(int saveFileNumber)
        {
            SaveFileNumber = saveFileNumber;
        }

        public int SaveFileNumber { get; }
    }
}
