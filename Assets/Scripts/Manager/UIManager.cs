using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager
{
    public string SetInGameText(string _string, float _data) => $"{_string} : {_data.ToString("F0")}";
    public string SetTotalScoreText(int _time, int _target) => $"{_time} X {_target} = {_time * _target}";
}
