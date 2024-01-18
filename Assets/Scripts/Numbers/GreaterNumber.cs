using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GreaterNumber : MonoBehaviour,INumber
{
    [SerializeField] private TextMeshPro numberText;

    [SerializeField] private int number;

    [SerializeField] private GameData gameData;

    private DestroyNumber destroyNumber;
    [SerializeField] private Transform fromTarget,bullseye;

    private void Start()
    {
        OnUpdateNumber();
        destroyNumber=GetComponent<DestroyNumber>();
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
        if(number<gameData.RoundedTime)
        {
            if(!gameData.isChallengerLevel)
                EventManager.Broadcast(GameEvent.OnMatchNumber);
            
            destroyNumber.CreateDestructionObject(fromTarget,bullseye);
        }
        else
        {
           if(!gameData.isChallengerLevel)
                return;
            else
                EventManager.Broadcast(GameEvent.OnChallengerGameOver);
        }

    }

    public void OnUpdateNumber()
    {
        numberText.SetText("> " + number.ToString());
    }

   
}
