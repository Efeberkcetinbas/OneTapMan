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

    [SerializeField] private Transform elevator1,elevator2;
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
            elevator1.transform.DOMoveY(elevator1.transform.position.y-2,1f).SetEase(ease);;
            elevator2.transform.DOMoveY(elevator2.transform.position.y+2,1f).SetEase(ease);;

        }
        
        else
        {
            elevator1.transform.DOMoveY(elevator1.transform.position.y+2,1f).SetEase(ease);;
            elevator2.transform.DOMoveY(elevator2.transform.position.y-2,1f).SetEase(ease);;

        }
    }
   
}
