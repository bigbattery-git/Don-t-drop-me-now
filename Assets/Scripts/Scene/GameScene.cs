using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseSceneEX
{
    protected override void Init()
    {
        Application.targetFrameRate = 60;

        if(!GameManager.Audio.IsCorrectBGM(AudioManager.AudioClipBGMAddress.Game))
        GameManager.Audio.SetAudioBGMClip(GameManager.Audio.SourceBGM, AudioManager.AudioClipBGMAddress.Game, true);
    }
}
