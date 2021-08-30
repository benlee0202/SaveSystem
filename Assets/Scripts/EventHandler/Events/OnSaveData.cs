using EventHandler.Interfaces;

namespace EventHandler.Events
{
    public readonly struct OnSaveData : IEventBase
    {
        public OnSaveData(int saveFileNumber)
        {
            SaveFileNumber = saveFileNumber;
        }

        public int SaveFileNumber { get; }
    }
}
