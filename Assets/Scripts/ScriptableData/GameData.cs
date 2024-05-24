using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 0)]
public class GameData : ScriptableObject 
{

    public int ReqMove;
    public int RoundedTime;
    public int totalWeightOurBowl;
    public int increaseScore;


    public int XP;
    public int BuffTime;

    public float Timer;

    public bool isGameEnd=false;
    public bool isStartTimer=false;

    public TimerTypes timerTypes;
}
