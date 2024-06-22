using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { NotRun, Run }
    #region static Data
    private static GameManager m_Instance;
    private static SpawnManager m_SpawnManager;
    private DataManager dataManager = new DataManager();
    private UIManager uiManager = new UIManager();
    private SceneMoveManager sceneMoveManager = new SceneMoveManager();
    private AudioManager audioManager = new AudioManager();

    public static GameManager Instance { get { Init(); return m_Instance; } }
    public static SpawnManager Spawn { get { Init(); return m_SpawnManager; } }
    public static DataManager Data { get { return m_Instance.dataManager; } }
    public static UIManager UI { get { return m_Instance.uiManager; } }
    public static SceneMoveManager SceneMove => m_Instance.sceneMoveManager;
    public static AudioManager Audio => m_Instance.audioManager;
    #endregion

    private PlayerScoreData scoreData = new PlayerScoreData();
    public static PlayerScoreData ScoreData => m_Instance.scoreData;
    public GameState gameState = GameState.NotRun;
    private float spawnTime = 10f;
    private float spawnTimeTimer;

    private float spawnItemTime;
    private float minSpawnItemTime = 8f;
    private float maxSpawnItemTime = 5f;
    private float spawnItemTimeTimer;
    public void Awake()
    {
        Init();
        audioManager.Init();
        ResetGame();
    }
    public static void Init()
    {
        GameObject obj = GameObject.Find("@Managers");
        if (obj == null)
        {
            obj = new GameObject("@Managers");
            obj.AddComponent<GameManager>();
            obj.AddComponent<SpawnManager>();
        }
        DontDestroyOnLoad(obj);
        m_Instance = obj.GetComponent<GameManager>();
        m_SpawnManager = obj.GetComponent<SpawnManager>();
    }
    private void Update()
    {
        if (gameState == GameState.Run)
        {
            scoreData.playerServivalTime += Time.deltaTime;
            spawnItemTimeTimer += Time.deltaTime;
            spawnTimeTimer += Time.deltaTime;

            if (spawnTimeTimer > spawnTime)
            {
                scoreData.playerTargetCount++;
                spawnTimeTimer = 0;
                m_SpawnManager.SpawnTarget();
            }
            if (spawnItemTimeTimer > spawnItemTime)
            {
                m_SpawnManager.SpawnItem((SpawnManager.ItemType)UnityEngine.Random.Range(0, (int)SpawnManager.ItemType.MaxSize));
                spawnItemTimeTimer = 0f;
                ResetSpawnItemTime();
            }
        }
    }
    private SpawnManager.ItemType SetItemType()
    {
        return SpawnManager.ItemType.GreenItem;
    }
    public void GameSet()
    {
        gameState = GameState.NotRun;
        Time.timeScale = 0;

        PlayerScoreData highScoreData = dataManager.LoadJsonData();
        if (scoreData.TotalScore() > highScoreData.TotalScore())
        {
            dataManager.SaveJsonData(scoreData);
        }

        Spawn.SpawnGameOverUI();
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        spawnTimeTimer = 0f;
        spawnItemTimeTimer = 0f;

        scoreData = new PlayerScoreData();
        scoreData.playerTargetCount = 1;

        ResetSpawnItemTime();
    }
    private void ResetSpawnItemTime() => spawnItemTime = UnityEngine.Random.Range(minSpawnItemTime, maxSpawnItemTime);
    public void GameExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }
}