using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueItem : Item
{
    public override void ActiveItemEffect()
    {
        GameObject obj;
        if(FindObjectOfType<Target>().gameObject == null)
        {
            return;
        }
        else
        {
            obj = FindObjectOfType<Target>().gameObject;
        }
        Destroy(obj);
    }
}