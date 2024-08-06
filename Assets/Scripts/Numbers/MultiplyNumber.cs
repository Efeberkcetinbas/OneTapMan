using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultiplyNumber : MonoBehaviour,INumber
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
        EventManager.AddHandler(GameEvent.OnCheckZero,OnCheckZero);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnCheckZero,OnCheckZero);
    }
    public void OnCheckZero()
    {
        if(gameData.totalWeightOurBowl%number==0)
        {
            EventManager.Broadcast(GameEvent.OnMatchNumber);
            
        }
        
        else
        {
            EventManager.Broadcast(GameEvent.OnDisMatchNumber);
        }

    }

   

    public void OnUpdateNumber()
    {
        numberText.SetText(" x " + number.ToString());
    }


   
    
}
