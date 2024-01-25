using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class  MarkerService : IMarkerService
{
    public static  MarkerService Instance { get; private set; }

    private List<IMarker> markers = new List<IMarker>();

    public void ActivateService()
    {
        Instance = this;
    }
    public void DeactivateService()
    {
        markers.Clear();
    }

    public T GetTransformMarker<T>() where T : IMarker
    {
        return markers.OfType<T>().FirstOrDefault();
    }

    public void SetMarker(IMarker marker)
    {
        markers.Add(marker);
    }
}

public interface IMarkerService: IService
{
    public T GetTransformMarker<T>() where T : IMarker;
}

public class Marker: MonoBehaviour, IMarker
{
    public void Awake()
    {
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
