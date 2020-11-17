using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SetText : MonoBehaviour
{
    public Player mPlayer;
    public string mBeforeName, mAfterName;
    public TextMeshProUGUI mTextField;


    public void addName()
    {
        mTextField.text = mBeforeName + " " +  mPlayer.GetName() +  mAfterName;
    }
    

}
