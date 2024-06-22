using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenItem : Item
{
    public override void ActiveItemEffect()
    {
        Target[] targets = FindObjectsOfType<Target>();

        foreach (Target target in targets)
        {
            target.SetGreenItem();
        } 
    }
}
