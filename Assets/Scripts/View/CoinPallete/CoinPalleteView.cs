using UnityEngine;
using System;
using TMPro;
using DG.Tweening;
using System.Collections;

public class CoinPalleteView : MonoBehaviour
{
	private Action _cointCollected;
    [SerializeField] private TMP_Text _coinCountTotalText;
    [SerializeField] private Transform _leftDoor;
    [SerializeField] private Transform _rightDoor;
    private Sequence _doorsOpenSequence;
    private int _coinCount;
    private float _closeDoorDelay = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DropedCoinView>(out DropedCoinView component))
        {
			_cointCollected?.Invoke();
        }
    }

    public void SetActionCoinCollecte(Action cointCollected)
    {
        _cointCollected = cointCollected;
    }

    public void UpdateView(int coinCountTotal)
    {
        _coinCountTotalText.text = coinCountTotal.ToString();
    }

    public void OpenDoors()
    {
        _doorsOpenSequence = DOTween.Sequence();
        _doorsOpenSequence.Append(_leftDoor.DORotate(new Vector3(0, 0, -90), 0.25f));
        _doorsOpenSequence.Join(_rightDoor.DORotate(new Vector3(0, 180, -90), 0.25f));
        _doorsOpenSequence.OnComplete(()=>StartCoroutine("CloseDoorDelay"));
    }

    public void CloseDoor()
    {
        _doorsOpenSequence = DOTween.Sequence();
        _doorsOpenSequence.Append(_leftDoor.DORotate(new Vector3(0, 0,0), 0.25f));
        _doorsOpenSequence.Join(_rightDoor.DORotate(new Vector3(0, 180,0), 0.25f));
    }

    private IEnumerator CloseDoorDelay()
    {
        yield return new WaitForSeconds(_closeDoorDelay);
        CloseDoor();
    }
}
