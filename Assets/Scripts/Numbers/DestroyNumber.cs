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

    [SerializeField] private GameObject pillow,character;

    private Vector3 firstPosition;


    private void Start()
    {
        firstPosition=transform.position;
        Debug.Log(gameObject.name + ": " + firstPosition);
    }

   

    private void Destruction()
    {
        gameObject.SetActive(false);
    }

    //Normal levels

    internal void CreateDestructionObject(Transform fromPos)
    {
        if(!gameData.isChallengerLevel)
        {
            Instantiate(destructionParticle,transform.position,Quaternion.identity);
            pillow.SetActive(false);
            character.SetActive(true);
            EventManager.Broadcast(GameEvent.OnIncreaseScore);
            EventManager.Broadcast(GameEvent.OnHitNumbers);
            transform.DOJump(fromPos.position,jumpPower,jumpNumber,duration).SetEase(ease).OnComplete(()=>{
                EventManager.Broadcast(GameEvent.OnNumberFall);
                transform.DOScale(Vector3.zero,0.25f).SetEase(ease).OnComplete(()=>{
                    Destruction();

                });
            });
        }

        else
        {
            EventManager.Broadcast(GameEvent.OnMatchChallengeNumber);
            //EventManager.Broadcast(GameEvent.OnUpdateChallenge);
        }
    }


    internal void OnRestartLevel()
    {
        transform.position=firstPosition;
        pillow.SetActive(true);
        character.SetActive(false);
        
    }

    //Challenger levels
    
}
