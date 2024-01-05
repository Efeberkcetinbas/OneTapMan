using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberTrigger : Obstacleable
{
    public NumberTrigger()
    {
        canStay=false;
        canDamageToPlayer=false;
    }

    internal override void DoAction(TriggerControl player)
    {
        Debug.Log("HIT ME " + gameObject.name);
    }

}
