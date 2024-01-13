using UnityEngine;
using System;
using TMPro;

public class CoinPalleteView : MonoBehaviour
{
	private Action _cointCollected;
    [SerializeField] private TMP_Text _coinCountTotalText;
    [SerializeField] private TMP_Text _coinCountAddedText;
    private int _coinCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DropedCoinView>(out DropedCoinView component))
        {
			_cointCollected?.Invoke();
            _coinCountAddedText.text = _coinCount++.ToString();
        }
    }

    public void SetActionCoinCollecte(Action cointCollected)
    {
        _cointCollected = cointCollected;
    }

    public void UpdateView(int coinCountTotal, int coinCountAdded)
    {
        _coinCountTotalText.text = coinCountTotal.ToString();
        _coinCountAddedText.text = coinCountAdded.ToString();
    }
}