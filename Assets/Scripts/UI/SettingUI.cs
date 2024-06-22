using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    public void TurnOnOffBGM(bool _isTurnOn) => GameManager.Audio.TurnOnOffBGM(_isTurnOn);
    public void TurnOnOffSFX(bool _isTurnOn) => GameManager.Audio.TurnOnOffSFX(_isTurnOn);
}
