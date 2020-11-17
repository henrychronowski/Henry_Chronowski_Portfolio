using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewPlayer", menuName = "Player")]
public class Player : ScriptableObject
{
    string mName;

    // Start is called before the first frame update
    void Start()
    {
        mName = null;  
    }

    public void SetName(string name)
    {
        mName = name;
    }

    public string GetName()
    {
        return mName;
    }
}
