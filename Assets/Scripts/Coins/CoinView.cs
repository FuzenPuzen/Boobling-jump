using TMPro;
using UnityEngine;

public class CoinView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI coinText;


    public void UpdateView(int coins)
    {
        coinText.text = coins.ToString();
    }
 
}
