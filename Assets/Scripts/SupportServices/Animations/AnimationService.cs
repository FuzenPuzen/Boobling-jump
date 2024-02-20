using EventBus;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Linq;

public interface IAnimationService : IService
{
	public void DeactivateService();
    public Animation PlayAnimation<Animation, Object>(AnimData AnimData = null)
        where Animation : Anim
        where Object : MonoBehaviour;

    public void PlayAnimation<Animation>(GameObject obj, AnimData animData = null)
        where Animation : Anim;
}

public class AnimationService : IAnimationService
{
    private List<MonoBehaviour> _animatedObjects = new();
	private EventBinding<OnAnimViewEnable> _onAnimViewEnable;
	private EventBinding<OnAnimViewDisable> _onAnimViewDisable;

    private Dictionary<Anim, MonoBehaviour> _objectAnims = new();

    public Animation PlayAnimation<Animation, ObjectType>(AnimData animData = null)
        where Animation : Anim
        where ObjectType : MonoBehaviour
    {
        var items = _animatedObjects.OfType<ObjectType>();
        Animation resultAnim = null;
        if (items.Count() >= 0)
            foreach (var item in items)
            {
                Animation anim = item.transform.GetOrAddComponent<Animation>();         
                anim.SetValues(animData ?? new());
                anim.Play();
                resultAnim = anim;
                _objectAnims.Add(resultAnim, item);
            }
        return resultAnim;
    }

    public void StopAnimation(Anim anim)
    {
        anim.Stop();
        _objectAnims.Remove(anim);
    }

    public void StopObjectAnimations(GameObject _object)
    {
        List<Anim> foundElements = _objectAnims.Where(pair => pair.Value == _object)
                                                .Select(pair => pair.Key)
                                                .ToList();
        foreach (var foundElement in foundElements)
        {
            StopAnimation(foundElement);
        }
    }

    public void PlayAnimation<Animation>(GameObject obj, AnimData animData = null)
        where Animation : Anim
    {
        Animation anim = obj.transform.GetOrAddComponent<Animation>();
        anim.SetValues(animData ?? new());
        anim.Play();
    }

    public void ActivateService()
	{
		_onAnimViewEnable = new(AddObjectsToList);
        _onAnimViewDisable = new(RemoveObjectsFromList);
    }

	public void DeactivateService() 
	{
        _onAnimViewEnable.Remove(AddObjectsToList);
        _onAnimViewDisable.Remove(RemoveObjectsFromList);
    }

	public void AddObjectsToList(OnAnimViewEnable onAnimViewEnable)
	{
        _animatedObjects.AddRange(onAnimViewEnable.Components);
    }

	public void RemoveObjectsFromList(OnAnimViewDisable onAnimViewDisable)
	{
		foreach(MonoBehaviour mono in onAnimViewDisable.Components)
			_animatedObjects.Remove(mono);
    }
}

public class AnimData
{
    public AnimType AnimType = AnimType.Default;
    public bool IsLoop;
    public float Duration = 0.2f;
}

public enum AnimType
{
    Default = Replace,
    Replace = 0,
    Add,
    Ignore,
    Stack
}
