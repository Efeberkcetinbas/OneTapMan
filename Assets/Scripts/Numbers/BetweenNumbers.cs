using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BetweenNumbers : MonoBehaviour,INumber
{
    [SerializeField] private TextMeshPro numberText;

    [SerializeField] private int leftNumber,rightNumber;

    [SerializeField] private GameData gameData;

    private DestroyNumber destroyNumber;

    [SerializeField] private Transform target;

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
        if(leftNumber<gameData.RoundedTime && gameData.RoundedTime<rightNumber)
        {
            if(!gameData.isChallengerLevel)
                EventManager.Broadcast(GameEvent.OnMatchNumber);
            
            destroyNumber.CreateDestructionObject(target);

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
        numberText.SetText(leftNumber.ToString() + " - " + rightNumber.ToString());
    }

    
}
