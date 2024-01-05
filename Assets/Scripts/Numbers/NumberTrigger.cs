using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NumberTrigger : Obstacleable
{
    [SerializeField] private GameObject[] particleEffect;
    [SerializeField] private Transform spawnPoint;

    private int randomIndex;

    private bool isHit=false;
    public NumberTrigger()
    {
        canStay=false;
        canDamageToPlayer=false;
    }

    internal override void DoAction(TriggerControl player)
    {
        if(!isHit)
        {
            randomIndex=Random.Range(0,particleEffect.Length);
            Instantiate(particleEffect[randomIndex],spawnPoint.position,Quaternion.identity);
            EventManager.Broadcast(GameEvent.OnHitNumbers);
            Debug.Log("HIT ME " + gameObject.name);
            isHit=true;
        }

    }

}
