using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnManager: MonoBehaviour
{
    public enum ItemType { GreenItem, BlueItem, RedItem, MaxSize}
    public void SpawnTarget()
    {
        string gameObj = "Target";
        Addressables.LoadAssetAsync<GameObject>(gameObj).Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                Target target = Instantiate(obj.Result).GetComponent<Target>();
                target.targetState = Target.TargetState.Spawn;
            }
            else
            {
                Debug.LogError("타겟 소환이 되지 않았습니다.");
            }
        };
    }
    public void SpawnItem(ItemType _itemType)
    {
        string gameObj = _itemType.ToString();
        Addressables.LoadAssetAsync<GameObject>(gameObj).Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                float width = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
                float height = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);

                Vector2 camSize = new Vector2(width, height);
                Instantiate(obj.Result, camSize , Quaternion.identity);
            }
            else
            {
                Debug.LogError("아이템 소환이 되지 않았습니다.");
            }
        };
    }
    public void SpawnGameOverUI()
    {
        Addressables.InstantiateAsync("GameOverUI");
    }
}