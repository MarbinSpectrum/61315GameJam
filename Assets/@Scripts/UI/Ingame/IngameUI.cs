using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class IngameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nowStageText;
    [SerializeField] private TextMeshProUGUI limitTimeText;
    [SerializeField] private RectTransform uiRect;
    
    private int limitTime = -1;

    private void Start()
    {
        SafeArea.SetSafeArea(uiRect);
    }

    private void Update()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (Managers.Game == null)
            return;
        
        int limitTimeValue = Managers.Game.Timer;
        if (limitTimeValue == limitTime)
            return;
        limitTime = limitTimeValue;
        
        int stage = Managers.Game.stageNum;
        nowStageText.text = $"Stage {stage}";
        
        limitTimeText.text = $"{limitTime}";
    }
}
