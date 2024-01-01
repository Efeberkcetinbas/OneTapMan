using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private GameData gameData;
    void Start()
    {
        gameData.isStartTimer=true;
    }

    void Update()
    {
        if(!gameData.isGameEnd)
            CheckStartStop();
    }

    private void CheckStartStop()
    {
        if(Input.touchCount>0)
        {
            Touch touch=Input.GetTouch(0);
            if(touch.phase==TouchPhase.Began)
            {
                gameData.isStartTimer=!gameData.isStartTimer;
                SendStartStop();
            }
            
        }
    }

    private void SendStartStop()
    {
        if(gameData.isStartTimer)
            EventManager.Broadcast(GameEvent.OnStartTimer);
        else
            EventManager.Broadcast(GameEvent.OnStopTimer);
    }
}
