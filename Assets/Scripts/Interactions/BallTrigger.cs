using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : Obstacleable
{
    public BallTrigger()
    {
        interactionTag="Killer";
    }

    internal override void DoAction(TriggerControl player)
    {
        Debug.Log("EXPLODE");
    }
}
