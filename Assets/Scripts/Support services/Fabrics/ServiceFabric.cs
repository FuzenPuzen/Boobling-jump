using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public interface IServiceFabric
{
    T Create<T>();
}

public class ServiceFabric: IServiceFabric
{
    private readonly DiContainer _container;

    [Inject]
    public ServiceFabric(DiContainer container)
    {
        _container = container;
    }

    public T Create<T>()
    {
        // Используем контейнер для создания экземпляра ConcreteFabricable
        T fabricableObject = _container.Instantiate<T>();

        // Возвращаем созданный объект
        return fabricableObject;
    }

}

