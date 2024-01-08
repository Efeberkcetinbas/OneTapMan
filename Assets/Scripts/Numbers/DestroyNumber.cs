using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DestroyNumber : MonoBehaviour
{   
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject destructionParticle;
    [SerializeField] private Ease ease;
    [SerializeField] private float jumpPower,duration;
    [SerializeField] private int jumpNumber;
    
   

    private void Destruction()
    {
        gameObject.SetActive(false);
    }

    internal void CreateDestructionObject(Transform fromPos)
    {
        
        Instantiate(destructionParticle,transform.position,Quaternion.identity);
        EventManager.Broadcast(GameEvent.OnIncreaseScore);
        EventManager.Broadcast(GameEvent.OnHitNumbers);
        transform.DOJump(fromPos.position,jumpPower,jumpNumber,duration).SetEase(ease).OnComplete(()=>{
            EventManager.Broadcast(GameEvent.OnNumberFall);
            transform.DOScale(Vector3.zero,0.25f).SetEase(ease).OnComplete(()=>{
                Destruction();

            });
        });
    }
    
}
