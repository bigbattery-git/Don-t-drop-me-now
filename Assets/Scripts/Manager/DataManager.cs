using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerScoreData
{
    public float playerServivalTime;
    public int playerTargetCount;

    public float TotalScore() => playerServivalTime * playerTargetCount;
}
public class DataManager
{    
    public void SaveJsonData(PlayerScoreData _data)
    {
        string jsonData =  JsonUtility.ToJson(_data, true);
        string path = Path.Combine(Application.persistentDataPath,".json");
        File.WriteAllText(path, jsonData);
    }
    public PlayerScoreData LoadJsonData()
    {
        string path = Path.Combine(Application.persistentDataPath, ".json");
        if (!File.Exists(path))
        {
            Debug.Log("해당 경로에 파일이 존재하지 않습니다.");
            return new PlayerScoreData();
        }
        return JsonUtility.FromJson<PlayerScoreData>(File.ReadAllText(path));
    }
    public bool HasJsonData(string _path)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{_path}.json");
        return File.Exists(path);
    }
}
