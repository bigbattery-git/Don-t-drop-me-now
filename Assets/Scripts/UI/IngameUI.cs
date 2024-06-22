using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private TextMeshProUGUI textCount;

    private void Update()
    {
        textTime.text = GameManager.UI.SetInGameText("Time", GameManager.ScoreData.playerServivalTime);
        textCount.text = GameManager.UI.SetInGameText("Count", GameManager.ScoreData.playerTargetCount);
    }
}
