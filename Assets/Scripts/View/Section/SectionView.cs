using UnityEngine;
using DG.Tweening;

public class SectionView : MonoBehaviour
{

    private Sequence _moveSequence;
    private float _endPosX = -42f;
    private float _movingTime = 5f;

    public void ActivateView()
    {
        StartMove();
    }

    public void StartMove()
    {
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOMoveX(_endPosX, _movingTime).SetEase(Ease.Linear));
    }

    public void SetValues(float movingTime, float endPosX)
    {
        _movingTime = movingTime;
        _endPosX = endPosX;       
    }

    private void OnTriggerExit(Collider other)
    {
        /*if (other.CompareTag("SectionActivator"))
        {
            // end of section
        }*/
    }
}
