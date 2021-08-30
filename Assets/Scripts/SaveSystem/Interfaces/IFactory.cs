using System;

namespace SaveSystem.Interfaces
{
    public interface IFactory<out T>
    {
        T GetProduct(Type type);
    }
}
