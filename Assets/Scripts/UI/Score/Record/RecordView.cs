using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _recordText;

    private RecordSLData _recordSLData;

    public void SetRecordData(RecordSLData recordSLData)
    {
        _recordSLData = recordSLData;
        _recordText.text = "Рекорд\n" + _recordSLData.GetRecord().ToString();
    }

    internal void HideView()
    {
        gameObject.SetActive(false);
    }
}
