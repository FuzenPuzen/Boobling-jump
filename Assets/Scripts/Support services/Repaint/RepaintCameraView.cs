using EventBus;
using UnityEngine;

public class RepaintCameraView : MonoBehaviour, IRepaint
{
	private Camera _camera;
    public void Awake()
    {
        _camera = GetComponent<Camera>();
        EventBus<OnRepaintAwake>.Raise();
    }

    public void Repaint(RepaintSOData repaintData)
    {
        _camera.backgroundColor = repaintData.backgroundColor;
    }
}
