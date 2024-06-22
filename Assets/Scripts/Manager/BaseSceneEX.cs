using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSceneEX : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Init();
        Init();
    }
    protected abstract void Init();
}
