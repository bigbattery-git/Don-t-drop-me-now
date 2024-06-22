using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedItem : Item
{
    public override void ActiveItemEffect()
    {
        GameManager.Instance.GameSet();
    }
}
