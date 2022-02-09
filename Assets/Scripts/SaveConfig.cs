using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveConfig 
{
    public List<float> box1;
    public List<float> box2;


    public SaveConfig(GameManeger scriptManeger)
    {
        box1 = scriptManeger.box1;
        box2 = scriptManeger.box2;
    }
}
