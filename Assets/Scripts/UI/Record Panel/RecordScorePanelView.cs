using TMPro;
using UnityEngine;

public class RecordScorePanelView : MonoBehaviour
{

    [SerializeField] private TMP_Text _recordText;

    public void UpdateView(int record)
    {
        _recordText.text = "ÐÅÊÎÐÄ\n" + record.ToString();
    }
    internal void HideView()
    {
        gameObject.SetActive(false);
    }
}
