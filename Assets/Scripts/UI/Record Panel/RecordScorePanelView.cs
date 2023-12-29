using TMPro;
using UnityEngine;

public class RecordScorePanelView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _recordText;

    public void UpdateView(int record)
    {
        _recordText.text = record.ToString();
    }
    internal void HideView()
    {
        gameObject.SetActive(false);
    }
}
