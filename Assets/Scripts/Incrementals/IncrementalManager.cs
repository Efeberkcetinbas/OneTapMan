using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IncrementalManager : MonoBehaviour
{
    //Will be implemented

    //Press Time Incremental
    //Increase Score Incremental
    public IncrementalData incrementalData;
    public LevelData levelData;
    public GameData gameData;

    [SerializeField] private Button pressButton,increaseScoreButton;

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
        incrementalData.IncreaseScorePrice = IncrementalEconomy(incrementalData.IncreaseScorePrice, GameEvent.OnUpdateIncreaseScore);
        OnIncrementalOpen();

    }

    public void UpdatePressTime()
    {
        //Economy Logic Here
        incrementalData.PressTimePrice =IncrementalEconomy(incrementalData.PressTimePrice,GameEvent.OnUpdatePressTime);
        OnIncrementalOpen();
    
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
    }

    private int IncrementalEconomy(int price, GameEvent gameEvent)
    {
        if (levelData.score >= price)
        {
            levelData.score -= price;
            price *= 2;
            EventManager.Broadcast(gameEvent);
            EventManager.Broadcast(GameEvent.OnUIUpdate);
            return price; // Return the updated price
        }
        else
        {
            Debug.Log("Not enough score to afford the upgrade.");
            return price; // Return the original price
        }
    }



}
