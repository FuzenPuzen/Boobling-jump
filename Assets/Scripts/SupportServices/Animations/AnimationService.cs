using EventBus;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Linq;
using DG.Tweening;
using Sequence = DG.Tweening.Sequence;

public interface IAnimationService : IService
{
	public void DeactivateService();
    public List<Anim> PlayAnimation<Animation, Object>(AnimData AnimData = null)
        where Animation : Anim
        where Object : MonoBehaviour;

    public Anim PlayAnimation<Animation>(GameObject obj, AnimData animData = null)
        where Animation : Anim;
}

public class AnimationService : IAnimationService
{
    private List<MonoBehaviour> _animatedObjects = new();
	private EventBinding<OnAnimViewEnable> _onAnimViewEnable;
	private EventBinding<OnAnimViewDisable> _onAnimViewDisable;

    private Dictionary<Anim, MonoBehaviour> _objectAnims = new();

    public List<Anim> PlayAnimation<Animation, ObjectType>(AnimData animData = null)
        where Animation : Anim
        where ObjectType : MonoBehaviour
    {
        List<Anim> anims = new List<Anim>();
        var items = _animatedObjects.OfType<ObjectType>();
        if (items.Count() >= 0)
            foreach (var item in items)
            {
                Animation anim = item.transform.GetOrAddComponent<Animation>();         
                anim.SetValues(animData ?? new());
                anim.Play();
                _objectAnims.TryAdd(anim, item);
                anims.Add(anim);
            }
        return anims;
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

    public Anim PlayAnimation<Animation>(GameObject obj, AnimData animData = null)
        where Animation : Anim
    {
        Animation anim = obj.transform.GetOrAddComponent<Animation>();
        anim.SetValues(animData ?? new());
        anim.Play();
        return anim;
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
public abstract class Anim : MonoBehaviour
{
    public Sequence _animSequence;
    public abstract void Play();
    public abstract void Stop();

    public abstract void SetValues(AnimData shakeAnimData);

    public void OnDestroy()
    {
        _animSequence.Kill();
    }
}



public enum AnimType
{
    Default = Replace,
    Replace = 0,
    Add,
    Ignore,
    Stack
}
