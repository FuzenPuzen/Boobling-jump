using System.Collections;
using TMPro;
using UnityEngine;

public class GiftScorePanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _giftScoreText;

    public void UpdateView(int newGeiftScore)
    {
        _giftScoreText.text = "ПОДАРОК\n" + newGeiftScore.ToString();
    }
    internal void HideView()
    {
        gameObject.SetActive(false);
    }

}