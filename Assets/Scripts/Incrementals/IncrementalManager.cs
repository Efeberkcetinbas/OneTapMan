using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IncrementalManager : MonoBehaviour
{
    //Will be implemented

    //Press Time Incremental
    //Increase Score Incremental
    public IncrementalData incrementalData;

    public void IncreaseScoreIcremental()
    {
        //Economy Logic Goes Here
        EventManager.Broadcast(GameEvent.OnUpdateIncreaseScore);
        EventManager.Broadcast(GameEvent.OnUIUpdate);
    }

    public void UpdatePressTime()
    {
        //Economy Logic Here
        EventManager.Broadcast(GameEvent.OnUpdatePressTime);
        EventManager.Broadcast(GameEvent.OnUIUpdate);
    }



}
