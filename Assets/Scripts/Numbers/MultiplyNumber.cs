using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultiplyNumber : MonoBehaviour,INumber
{
    [SerializeField] private TextMeshPro numberText;

    [SerializeField] private int number;

    [SerializeField] private GameData gameData;

    private DestroyNumber destroyNumber;

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
        if(gameData.RoundedTime%number==0)
        {
            EventManager.Broadcast(GameEvent.OnHitNumber);
            Debug.Log("MULTIPLYYY");
            destroyNumber.MakeDestruction();
        }
        else
            return;
    }

    public void OnUpdateNumber()
    {
        numberText.SetText(" x " + number.ToString());
    }

    
}