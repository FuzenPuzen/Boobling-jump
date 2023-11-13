using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _recordText;

    private RecordSLDataService _recordSLData;

    public void SetRecordData(RecordSLDataService recordSLData)
    {
        _recordSLData = recordSLData;
        _recordText.text = "Рекорд\n" + _recordSLData.GetRecord().ToString();
    }

    internal void HideView()
    {
        gameObject.SetActive(false);
    }
}
