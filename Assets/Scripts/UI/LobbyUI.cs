using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;
public class LobbyUI : MonoBehaviour
{
    private Canvas canvas;

    [SerializeField] private Button btnGoGameScene;
    [SerializeField] private Button btnTest;

    [SerializeField] private GameObject middleButton;
    private void Start()
    {
        canvas= GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;

        btnGoGameScene.onClick.AddListener(() => GameManager.SceneMove.MoveScene(SceneMoveManager.SceneName.GameScene));
        btnTest.onClick.AddListener(() => GameManager.Instance.GameExit());

        Button[] btn = middleButton.transform.GetComponentsInChildren<Button>();

        foreach(Button bt in btn)
        {
            bt.onClick.AddListener(OnClickButtonSound);
        }
    }
    private void OnClickButtonSound()
    {
        GameManager.Audio.SetAudioSFXClip(GameManager.Audio.SourceSFX, AudioManager.AudioClipSFXAddress.Button);
    }
}