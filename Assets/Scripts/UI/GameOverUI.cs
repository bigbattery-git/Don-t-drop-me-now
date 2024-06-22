using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private TextMeshProUGUI txtHighScore;

    [SerializeField] private Button btnRestart;
    [SerializeField] private Button btnTitle;

    private Canvas canvas;
    private void Awake()
    {
        canvas = GetComponent<Canvas>();

        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;

        btnRestart.onClick.AddListener(() => GameManager.SceneMove.MoveScene(SceneMoveManager.SceneName.GameScene));
        btnRestart.onClick.AddListener(OnClickButtonSound);
        btnTitle.onClick.AddListener(() => GameManager.SceneMove.MoveScene(SceneMoveManager.SceneName.LobbyScene));
        btnTitle.onClick.AddListener(OnClickButtonSound);


        SetTxt();
    }
    private void SetTxt()
    {
        PlayerScoreData highScoreData = GameManager.Data.LoadJsonData();

        txtScore.text = GameManager.UI.SetTotalScoreText(Mathf.FloorToInt(GameManager.ScoreData.playerServivalTime), GameManager.ScoreData.playerTargetCount);
        txtHighScore.text = GameManager.UI.SetTotalScoreText(Mathf.FloorToInt(highScoreData.playerServivalTime), highScoreData.playerTargetCount);
    }
    private void OnClickButtonSound()
    {
        GameManager.Audio.SetAudioSFXClip(GameManager.Audio.SourceSFX, AudioManager.AudioClipSFXAddress.Button);
    }
}
