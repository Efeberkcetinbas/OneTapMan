using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LessNumber : MonoBehaviour,INumber
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
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);
    }
    public void OnStopTimer()
    {
        if(number>gameData.RoundedTime)
        {
            Debug.Log("POINT AND EXECUTED");
            Destroy(gameObject);
        }
        else
            return;
    }

    public void OnUpdateNumber()
    {
        numberText.SetText("< " + number.ToString());
    }
}
