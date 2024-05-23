using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BetweenNumbers : MonoBehaviour,INumber
{
    [SerializeField] private TextMeshPro numberText;

    [SerializeField] private int leftNumber,rightNumber;

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
        if(leftNumber<gameData.RoundedTime && gameData.RoundedTime<rightNumber)
        {
            EventManager.Broadcast(GameEvent.OnMatchNumber);

        }
        

    }

    public void OnUpdateNumber()
    {
        numberText.SetText(leftNumber.ToString() + " - " + rightNumber.ToString());
    }

    
}
