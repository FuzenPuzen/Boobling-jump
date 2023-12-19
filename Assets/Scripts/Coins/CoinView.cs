using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI coinText;
    private ICoinData _coinData;

    public void SetCoinData(ICoinData coinData)
    {
        _coinData = coinData;
    }

    public void UpdateView()
    {
        coinText.text = _coinData.GetCoins().ToString();
    }
 
}
