using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public interface IServiceFabric
{
    T Init<T>();
    public object Init(Type Type);
}

public class ServiceFabric: IServiceFabric
{
    private readonly DiContainer _container;

    [Inject]
    public ServiceFabric(DiContainer container)
    {
        _container = container;
    }

    public T Init<T>()
    {
        // ���������� ��������� ��� �������� ���������� ConcreteFabricable
        T fabricableObject = _container.Instantiate<T>();

        // ���������� ��������� ������
        return fabricableObject;
    }

    public object Init(Type Type)
    {
        return _container.Instantiate(Type);
    }

}

