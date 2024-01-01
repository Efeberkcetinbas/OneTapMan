using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClockTime : MonoBehaviour
{
    public float CurrentTime;
    public TimerTypes timerType;
    
    [SerializeField] private GameData gameData;

    private void OnEnable()
    {
        gameData.timerTypes=timerType;
    }

    
}
