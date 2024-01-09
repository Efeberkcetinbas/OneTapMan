using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 0)]
public class GameData : ScriptableObject 
{

    public int ReqMove;
    public int NeededNumber;
    public int RoundedTime;

    public float Timer;

    public bool isGameEnd=false;
    public bool isStartTimer=false;
    public bool isChallengerLevel=false;

    public TimerTypes timerTypes;
}
