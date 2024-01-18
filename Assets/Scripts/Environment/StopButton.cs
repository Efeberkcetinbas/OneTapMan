using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StopButton : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle,changeParticle;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    

    [SerializeField] private Color startColor,stopColor;
    [SerializeField] private Transform GHand,fist,hand;
    [SerializeField] private Ease ease;


    [SerializeField] private float duration;

    

    
    private void Start()
    {
        //OnNextLevel();
    }
    
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStartTimer,OnStartTimer);
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnThrowSword,OnThrowSword);

    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStartTimer,OnStartTimer);
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnThrowSword,OnThrowSword);
    }

    private void OnStartTimer()
    {
        HitButton(startColor);
        SetActivity(fist.gameObject,hand.gameObject,true,false);
    }

    private void OnStopTimer()
    {
        HitButton(stopColor);
        SetActivity(fist.gameObject,hand.gameObject,false,true);
        
    }
    private void OnNextLevel()
    {
        HitButton(stopColor);
    }
    private void HitButton(Color color)
    {
        //meshRenderer.material.DOColor(color,duration);
    }

    private void OnThrowSword()
    {
        GHand.DOScale(Vector3.one*1.25f,.15f).OnComplete(()=>
        {
            GHand.DOScale(Vector3.one,.15f).SetEase(ease);
            particle.Play();
        });
    }


    private void SetActivity(GameObject first,GameObject second,bool val1,bool val2)
    {
        first.SetActive(val1);
        second.SetActive(val2);
        changeParticle.Play();
    }
}
