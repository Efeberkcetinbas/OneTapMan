using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OneNumber : MonoBehaviour, INumber
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
        if(number==gameData.RoundedTime)
        {
            EventManager.Broadcast(GameEvent.OnHitNumber);
            Debug.Log("POINT AND EXECUTED");
            destroyNumber.MakeDestruction();
        }
        else
            return;
    }

    public void OnUpdateNumber()
    {
        numberText.SetText(number.ToString());
    }

    
}
