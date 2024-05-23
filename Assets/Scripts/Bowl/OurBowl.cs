using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurBowl : MonoBehaviour, IBowl
{
    [Header("DATA")]
    [SerializeField] private GameData gameData;



    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);

    }

    private void OnStopTimer()
    {
        CalculateWeight();
    }
    public void CalculateWeight()
    {
        gameData.totalWeightOurBowl+=gameData.RoundedTime;
        EventManager.Broadcast(GameEvent.OnUpdateOurWeightUI);
    }


}
