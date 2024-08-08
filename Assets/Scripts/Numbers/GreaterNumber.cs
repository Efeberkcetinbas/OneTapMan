using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GreaterNumber : MonoBehaviour,INumber
{
    [SerializeField] private TextMeshPro numberText;

    [SerializeField] private int number;

    [SerializeField] private GameData gameData;

    [SerializeField] private Transform glassElevator,otherElevator;
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
        CheckScale();
        
        if(number<gameData.totalWeightOurBowl)
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
        numberText.SetText(number.ToString());
    }

    private void CheckScale()
    {
        if(number>gameData.totalWeightOurBowl)
        {
            glassElevator.transform.DOMoveY(glassElevator.transform.position.y-2,1f);
            otherElevator.transform.DOMoveY(otherElevator.transform.position.y+2,1f).SetEase(ease);;

        }
        
        else
        {
            glassElevator.transform.DOMoveY(glassElevator.transform.position.y+2,1f);
            otherElevator.transform.DOMoveY(otherElevator.transform.position.y-2,1f).SetEase(ease);;

        }
    }
   
}
