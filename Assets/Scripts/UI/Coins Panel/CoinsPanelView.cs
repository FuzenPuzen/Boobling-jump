using TMPro;
using UnityEngine;

public class CoinsPanelView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI coinText;


    public void UpdateView(int coins)
    {
        coinText.text = coins.ToString();
    }
 
}
