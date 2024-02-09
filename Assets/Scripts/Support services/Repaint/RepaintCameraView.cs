using EventBus;
using UnityEngine;

public class RepaintCameraView : MonoBehaviour, IRepaint
{
	private Camera _camera;
    public void OnEnable()
    {
        _camera = GetComponent<Camera>();
        Debug.Log("Repaint work");
        EventBus<OnRepaintAwake>.Raise(new() { marker = this });
    }

    public void Repaint(RepaintSOData repaintData)
    {
        _camera.backgroundColor = repaintData.backgroundColor;
    }
}
