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
    [SerializeField] private GameObject destructionObject;

    private Vector3 firstPosition;

    private Sequence sequence;


    private void Start()
    {
        firstPosition=transform.position;
    }

   

    private void Destruction()
    {
        gameObject.SetActive(false);
    }

    //Normal levels

    internal void CreateDestructionObject(Transform fromPos,Transform targetPos)
    {
        if(!gameData.isChallengerLevel)
        {
            GameObject destructionClone=Instantiate(destructionObject);
            EventManager.Broadcast(GameEvent.OnThrowSword);
            //Event firlat. El kapansin
            destructionClone.transform.position=fromPos.position;
            /*destructionClone.transform.DOMove(targetPos.position,0.25f).SetEase(ease).OnComplete(()=>{
                Destroy(destructionClone);
                Destruction();
            });*/
            destructionClone.transform.localScale=Vector3.zero;
            destructionClone.transform.DOScale(Vector3.one,.5f).OnComplete(()=>{
                Sequence sequence=DOTween.Sequence();
                
                sequence.Append(destructionClone.transform.DOMove(targetPos.position,duration)
                    .SetEase(ease))
                    .Join(destructionClone.transform.DORotate(new Vector3(360,0,0),duration,RotateMode.FastBeyond360));

                

                sequence.OnComplete(()=>{
                    EventManager.Broadcast(GameEvent.OnHitSword);
                    Destroy(destructionClone);
                    Destruction();
                });
            });
            
            //Collider ile carpisma gerceklesir

            /*Instantiate(destructionParticle,transform.position,Quaternion.identity);
            pillow.SetActive(false);
            character.SetActive(true);
            EventManager.Broadcast(GameEvent.OnIncreaseScore);
            EventManager.Broadcast(GameEvent.OnHitNumbers);
            transform.DOJump(fromPos.position,jumpPower,jumpNumber,duration).SetEase(ease).OnComplete(()=>{
                EventManager.Broadcast(GameEvent.OnNumberFall);
                transform.DOScale(Vector3.zero,0.25f).SetEase(ease).OnComplete(()=>{
                    Destruction();

                });
            });*/
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
