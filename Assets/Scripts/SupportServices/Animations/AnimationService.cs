using Zenject;
using EventBus;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Linq;
using System;

public interface IAnimationService : IService
{
	public void DeactivateService();
    public void PlayAnimation<Animation, Object>(AnimData AnimData = null)
        where Animation : Anim
        where Object : MonoBehaviour;

    public void Shake(GameObject obj, ShakeAnimData shakeAnimData = null);
}

public class AnimationService : IAnimationService
{
    private List<MonoBehaviour> _animatedObjects = new();
	private EventBinding<OnAnimViewEnable> _onAnimViewEnable;
	private EventBinding<OnAnimViewDisable> _onAnimViewDisable;

    public void PlayAnimation<Animation, Object>(AnimData animData = null)
        where Animation : Anim
        where Object : MonoBehaviour
    {
        var items = _animatedObjects.OfType<Object>();
        if (items.Count() >= 0)
            foreach (var item in items)
            {
                Animation anim = item.transform.GetOrAddComponent<Animation>();               
                anim.SetValues(animData ?? new());
                anim.Play();
            }
    }

    public void Shake(GameObject obj, ShakeAnimData shakeAnimData = null)
    {
        ShakeAnim shakeAnim = obj.transform.GetOrAddComponent<ShakeAnim>();
        shakeAnim.SetValues(shakeAnimData);
        shakeAnim.Play();
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

}
