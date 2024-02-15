using EventBus;
using UnityEngine;

public class RepaintRenderView : MonoBehaviour, IRepaint
{
	private MeshRenderer _meshRenderer;

    public void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        EventBus<OnRepaintAwake>.Raise(new() { marker = this });
    }

	public void Repaint(RepaintSOData repaintData)
	{
        _meshRenderer.material.color = repaintData.main;
	}
}