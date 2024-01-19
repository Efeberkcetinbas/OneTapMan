using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Burst.Intrinsics;


public class DestroyNumber : MonoBehaviour
{   
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject destructionParticle;
    [SerializeField] private Ease ease;
    [SerializeField] private float jumpPower,duration;
    [SerializeField] private int jumpNumber;

    [SerializeField] private GameObject pillow;
    [SerializeField] private GameObject destructionObject;

    private Vector3 firstPosition;

    [SerializeField] private Transform target;

    //Instantiate et bunu
    [SerializeField] private Window window;


    private void Start()
    {
        firstPosition=transform.position;
    }

   

    private void Destruction()
    {
        window.transform.SetParent(null);
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
            destructionClone.transform.position=target.position;
            /*destructionClone.transform.DOMove(targetPos.position,0.25f).SetEase(ease).OnComplete(()=>{
                Destroy(destructionClone);
                Destruction();
            });*/
            //destructionClone.transform.localScale=Vector3.zero;

            // !!!!!!!!!!!!! BU AYARLAR OYNANABILIR. EN UYGUN HISSIYATA KADAR
            destructionClone.transform.DOMove(fromPos.position,.75f).OnComplete(()=>{
                destructionClone.transform.DORotate(new Vector3(360,0,0),0.5f,RotateMode.FastBeyond360).OnComplete(()=>{
                    destructionClone.transform.LookAt(transform.position);
                    Sequence sequence=DOTween.Sequence();
                    
                    //Look at ile yaptir ki sword neresi olursa olsun duzgun baksin Hedefe
                    sequence.Append(destructionClone.transform.DOMove(targetPos.position,duration)
                        .SetEase(ease));

                    

                    sequence.OnComplete(()=>{
                        EventManager.Broadcast(GameEvent.OnHitSword);
                        window.OnHitSword();
                        Instantiate(destructionParticle,transform.position,Quaternion.identity);
                        Destroy(destructionClone);
                        Destruction();
                    });
                });
            });
            
            //Collider ile carpisma gerceklesir

            /*
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
        
    }

    //Challenger levels
    
}
