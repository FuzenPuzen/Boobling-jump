using System.Collections.Generic;
using UnityEngine;
using EventBus;

public class AnimView : MonoBehaviour
{
    private List<MonoBehaviour> animViews = new();

    public void OnEnable()
    {
        animViews = FindAnimViewComponents();
        EventBus<OnAnimViewEnable>.Raise(new() { Components = animViews });
    }

    public void OnDisable()
    {
        EventBus<OnAnimViewDisable>.Raise(new() { Components = animViews });
    }

    public List<MonoBehaviour> FindAnimViewComponents()
    {
        List<MonoBehaviour> animViewComponents = new List<MonoBehaviour>();

        // �������� ��� ���������� �� ������� �������
        MonoBehaviour[] components = GetComponents<MonoBehaviour>();

        // ���������� ���������� � ��������� ��, ������� ��������� ��������� IAnimView
        foreach (MonoBehaviour component in components)
        {
            if (component is IAnimView)
            {
                animViewComponents.Add(component);
            }
        }

        return animViewComponents;
    }

}

public interface IAnimView 
{

}

