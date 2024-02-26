using EventBus;
using UnityEngine;

public class RepaintCameraView : MonoBehaviour, IRepaint
{
	private Camera _camera;
    public void OnEnable()
    {
        _camera = GetComponent<Camera>();
        EventBus<OnRepaintAwake>.Raise(new() { Marker = this });
    }

    public void Repaint(RepaintSOData repaintData)
    {
        _camera.backgroundColor = repaintData.backgroundColor;
    }
}
