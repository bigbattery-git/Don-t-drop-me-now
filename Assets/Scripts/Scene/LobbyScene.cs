using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseSceneEX
{
    protected override void Init()
    {
        if(!GameManager.Audio.IsCorrectBGM(AudioManager.AudioClipBGMAddress.Main))
        GameManager.Audio.SetAudioBGMClip(GameManager.Audio.SourceBGM, AudioManager.AudioClipBGMAddress.Main, true);
    }
}