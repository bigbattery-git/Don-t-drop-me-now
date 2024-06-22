using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject,3f);
    }
    private void OnMouseDown()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Run)
            return;

        ActiveItemEffect();
        GameManager.Audio.SetAudioSFXClip(GameManager.Audio.SourceSFX, AudioManager.AudioClipSFXAddress.Item);
        Destroy(gameObject);
    }
    public abstract void ActiveItemEffect();
}
