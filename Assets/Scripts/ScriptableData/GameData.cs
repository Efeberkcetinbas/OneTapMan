using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 0)]
public class GameData : ScriptableObject 
{

    public int score;
    public int increaseScore;
    public int tempMove;
    public int ReqMove;
    public int RoundedTime;

    public float Timer;

    public bool isGameEnd=false;
    public bool isStartTimer=false;

    public TimerTypes timerTypes;
}
