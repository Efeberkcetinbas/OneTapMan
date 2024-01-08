using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

//Enum ile timer type yap ilk leveller basit yavasca artarken ilerki levellerde hizla arttir.
public enum TimerTypes
{
    basicTimer,
    milliTimer,
}

public class ClockManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    private bool isStop;

    //Timer
    [Header("TIME")]
    private float cTime;
    private int multiply;
    [SerializeField] private TextMeshProUGUI timerText;
    private TimerTypes timerTypes;
    private void Start()
    {
        OnNextLevel();
    }

    private void Update()
    {
        if(!gameData.isGameEnd)
            SetTimer();
    }

    private void SetTimer()
    {
        if(!isStop)
        {
            if(cTime<=gameData.Timer)
            {
                cTime+=Time.deltaTime*multiply;
                gameData.RoundedTime=Mathf.RoundToInt(cTime);
                timerText.SetText(gameData.RoundedTime.ToString());
            }
            else
            {
                cTime=0;
            }
            
        }
        
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnStartTimer,OnStartTimer);
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);

    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnStartTimer,OnStartTimer);
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);

    }

    private void OnNextLevel()
    {
        gameData.Timer=FindObjectOfType<LevelClockTime>().CurrentTime;
        gameData.timerTypes=FindObjectOfType<LevelClockTime>().timerType;
        CheckTimerType();
        /*isStop=true;
        gameData.RoundedTime=0;*/

        Debug.Log(multiply);
    }


    private void CheckTimerType()
    {
        switch(gameData.timerTypes)
        {
            case TimerTypes.basicTimer:
                Debug.Log("BURAYA");
                multiply=1;
                break;
            case TimerTypes.milliTimer:
                Debug.Log("ORAYA");
                multiply=75;
                break;
        }
        
    }

    private void OnStartTimer()
    {   
        isStop=false;
    }

    private void OnStopTimer()
    {
        isStop=true;
        ScaleUP();
    }

    private void ScaleUP()
    {
        timerText.transform.DOScale(Vector3.one*1.5f,.25f).OnComplete(()=>timerText.transform.DOScale(Vector3.one,.25f));
    }
}
