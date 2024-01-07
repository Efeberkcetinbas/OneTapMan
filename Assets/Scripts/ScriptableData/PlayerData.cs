using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 1)]
public class PlayerData : ScriptableObject 
{
    public bool playerCanMove=true;
    public int selectedBallIndex;

    public void SaveData()
    {

    }

    public void LoadData()
    {

    }
    
}
