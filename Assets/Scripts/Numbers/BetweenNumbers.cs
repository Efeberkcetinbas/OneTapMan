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
        EventManager.AddHandler(GameEvent.OnCheckZero,OnCheckZero);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnCheckZero,OnCheckZero);
    }
    public void OnCheckZero()
    {
        if(leftNumber<gameData.totalWeightOurBowl && gameData.totalWeightOurBowl<rightNumber)
            EventManager.Broadcast(GameEvent.OnMatchNumber);
        else
            EventManager.Broadcast(GameEvent.OnDisMatchNumber);
        

    }

    public void OnUpdateNumber()
    {
        numberText.SetText(leftNumber.ToString() + " - " + rightNumber.ToString());
    }

    
}
