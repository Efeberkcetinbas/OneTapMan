using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRewarded : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    public void PlayRewardedVideo()
    {
        Debug.Log("REWARDED VIDEO IS PLAYING");
        Debug.Log("REWARDED VIDEO IS FINISHED");
        UpdateRequirementMoveNumber();
    }


    private void UpdateRequirementMoveNumber()
    {
        gameData.ReqMove+=5;
        EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);
    }
}
