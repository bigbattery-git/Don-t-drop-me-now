using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveManager
{
    public enum SceneName
    {
        LobbyScene, GameScene
    }
    public void MoveScene(SceneName _sceneName)
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene(_sceneName.ToString());
    }
}
