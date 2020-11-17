using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NameTransfer : MonoBehaviour
{
    public Player mPlayer;
    public TextMeshProUGUI textMesh;
    public void SetPlayerName(string name)
    {
        mPlayer.SetName(textMesh.text);
    }
}
