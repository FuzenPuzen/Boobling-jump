using UnityEngine;
using DG.Tweening;

public class SectionView : MonoBehaviour
{

    private Sequence _moveSequence;
    private float _endPosX = -22f;
    private float _movingTime = 5f;

    private void Start()
    {
        StartMove();
    }

    public void StartMove()
    {
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOMoveX(_endPosX, _movingTime).SetEase(Ease.Linear));
    }

    private void OnTriggerExit(Collider other)
    {
        /*if (other.CompareTag("SectionActivator"))
        {
            // end of section
        }*/
    }
}
