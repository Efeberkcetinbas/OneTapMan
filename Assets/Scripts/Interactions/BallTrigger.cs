using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : Obstacleable
{
    private bool isExploded=false;

    //[SerializeField] private ParticleSystem particle;
    
    public BallTrigger()
    {
        interactionTag="Killer";
    }

    internal override void DoAction(TriggerControl player)
    {
        if(!isExploded)
        {
            Debug.Log("EXPLODE");
            //particle.Play();
            //gameObject.SetActive(false);
            isExploded=true;
        }
        
    }
}
