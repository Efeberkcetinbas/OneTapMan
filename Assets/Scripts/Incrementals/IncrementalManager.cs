using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class IncrementalManager : MonoBehaviour
{
    //Will be implemented

    //Press Time Incremental
    //Increase Score Incremental
    public IncrementalData incrementalData;
    public LevelData levelData;
    public GameData gameData;

    [Header("Inc/Buttons")]
    [SerializeField] private Button pressButton;
    [SerializeField] private Button increaseScoreButton;
    [SerializeField] private Button buffTimeButton;

    
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnIncrementalOpen,OnIncrementalOpen);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnIncrementalOpen,OnIncrementalOpen);
    }


    public void IncreaseScoreIcremental()
    {
        //Economy Logic Goes Here
        incrementalData.IncreaseScorePrice = IncrementalEconomy(incrementalData.IncreaseScorePrice, GameEvent.OnUpdateIncreaseScore,2);
        OnIncrementalOpen();
        incrementalData.increaseScore++;
        incrementalData.SaveDataWithJson();

    }

    public void UpdatePressTime()
    {
        //Economy Logic Here
        incrementalData.PressTimePrice =IncrementalEconomy(incrementalData.PressTimePrice,GameEvent.OnUpdatePressTime,2);
        OnIncrementalOpen();
        incrementalData.pressTime-=0.2f;
        incrementalData.SaveDataWithJson();
    }

    public void IncreaseBuffTime()
    {
        //incrementalData.BuffTimePrice =IncrementalEconomy(incrementalData.BuffTimePrice,GameEvent.OnUpdateBuffTime,3);
        OnIncrementalOpen();
        gameData.BuffTime+=5;
        incrementalData.SaveDataWithJson();
    }

    private void OnIncrementalOpen()
    {
        if(levelData.score>=incrementalData.IncreaseScorePrice)
            increaseScoreButton.interactable=true;
        else
            increaseScoreButton.interactable=false;

        if(levelData.score>=incrementalData.PressTimePrice)
            pressButton.interactable=true;
        else
            pressButton.interactable=false;

        EventManager.Broadcast(GameEvent.OnUIUpdate);
        EventManager.Broadcast(GameEvent.OnUpdatePressTime);
        EventManager.Broadcast(GameEvent.OnUpdateIncreaseScore);
        //EventManager.Broadcast(GameEvent.OnUpdateBuffTime);
    }

    private int IncrementalEconomy(int price, GameEvent gameEvent,int multiplyPrice)
    {
        if (levelData.score >= price)
        {
            levelData.score -= price;
            price *= multiplyPrice;
            EventManager.Broadcast(gameEvent);
            EventManager.Broadcast(GameEvent.OnUIUpdate);
            levelData.SaveDataWithJson();
            return price; // Return the updated price
        }
        else
        {
            Debug.Log("Not enough score to afford the upgrade.");
            return price; // Return the original price
        }
    }



}
