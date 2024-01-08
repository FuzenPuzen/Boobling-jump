using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MarkerService : IMarkerService
{
    public static MarkerService Instance { get; private set; }

    private List<IMarker> markers = new List<IMarker>();

    public void ActivateService()
    {
        Instance = this;
        MonoBehaviour.print("MarkerServiceActivate");
    }

    public T GetTransformMarker<T>()
    {
        return (T)markers.OfType<T>();
    }

    public void SetMarker(IMarker marker)
    {
        MonoBehaviour.print($"AddNewMarker: {marker}");
        markers.Add(marker);
    }
}

public interface IMarkerService: IService
{
    public T GetTransformMarker<T>();
}

public class Marker: MonoBehaviour, IMarker
{
    public void Start()
    {
        print("Marker Start");
        ActivateMarker();
    }

    public void ActivateMarker()
    {
        MarkerService.Instance?.SetMarker(this);
    }
}

public interface IMarker
{
    public void ActivateMarker();
}
