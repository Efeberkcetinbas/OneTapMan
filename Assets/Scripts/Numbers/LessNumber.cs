using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LessNumber : MonoBehaviour,INumber
{
    [SerializeField] private TextMeshPro numberText;

    [SerializeField] private int number;

    [SerializeField] private GameData gameData;


    private void Start()
    {
        OnUpdateNumber();
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);
    }
    public void OnStopTimer()
    {
        if(number>gameData.RoundedTime)
        {
            if(!gameData.isChallengerLevel)
                EventManager.Broadcast(GameEvent.OnMatchNumber);


        }
       
        
    }

    public void OnUpdateNumber()
    {
        numberText.SetText("< " + number.ToString());
    }

    
}
