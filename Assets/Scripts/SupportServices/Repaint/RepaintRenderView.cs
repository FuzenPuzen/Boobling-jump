using EventBus;
using UnityEngine;

public class RepaintRenderView : MonoBehaviour, IRepaint
{
    [SerializeField] private RepainType _repainType;
	private MeshRenderer _meshRenderer;

    public void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        EventBus<OnRepaintAwake>.Raise(new() { Marker = this });
    }

	public void Repaint(RepaintSOData repaintData)
	{
        switch(_repainType)
        {
            case RepainType.Main:
                _meshRenderer.material.color = repaintData.main;
                break;
            case RepainType.Second:
                _meshRenderer.material.color = repaintData.second;
                break;
            case RepainType.Accent:
                _meshRenderer.material.color = repaintData.accent;
                break;
        }
	}

    public enum RepainType
    {
        Main,
        Second,
        Accent
    }
}