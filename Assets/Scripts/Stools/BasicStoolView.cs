using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BasicStoolView : MonoBehaviour, IStoolView
{

    private float _moveTarget = 26;
    private float _movingTime = 10f;

    public virtual void DestroyStool()
    {
        StartCoroutine(DestroyView());
    }

    private IEnumerator DestroyView()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);

    }

    public virtual void ActivateView()
    {
        StartMove();
        DestroyStool();
    }

    public virtual void StartMove()
    {
        transform.DOMoveX(_moveTarget, _movingTime);
    }

}
