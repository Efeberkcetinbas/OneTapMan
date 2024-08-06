using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BetweenNumbers : MonoBehaviour,INumber
{
    [SerializeField] private TextMeshPro numberText1;
    [SerializeField] private TextMeshPro numberText2;

    [SerializeField] private int leftNumber,rightNumber;

    [SerializeField] private GameData gameData;
    [SerializeField] private Transform elevator1,elevator2,elevator3;

    [SerializeField] private Ease ease;

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
        CheckLeft();
        CheckRight();

        if(leftNumber<gameData.totalWeightOurBowl && gameData.totalWeightOurBowl<rightNumber)
            EventManager.Broadcast(GameEvent.OnMatchNumber);
        else
            EventManager.Broadcast(GameEvent.OnDisMatchNumber);
        

    }

    
    public void OnUpdateNumber()
    {
        numberText1.SetText(leftNumber.ToString());
        numberText2.SetText(rightNumber.ToString());
    }

    private void CheckLeft()
    {
        if(leftNumber>gameData.totalWeightOurBowl)
        {
            elevator1.transform.DOMoveY(elevator1.transform.position.y-2,1f).SetEase(ease);;
            
        }
        
        else
        {
            elevator1.transform.DOMoveY(elevator1.transform.position.y+2,1f).SetEase(ease);;
        }


        
    }

    private void CheckRight()
    {
        if(rightNumber>gameData.totalWeightOurBowl)
        {
            elevator2.transform.DOMoveY(elevator1.transform.position.y-2,1f).SetEase(ease);;
        }
        
        else
        {
            elevator2.transform.DOMoveY(elevator1.transform.position.y+2,1f).SetEase(ease);;
        }
    }
   
    
}
