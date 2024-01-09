using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crate : MonoBehaviour
{
    [SerializeField] private ParticleSystem inFruitParticle;

    [SerializeField] private float duration;

    [SerializeField] private Transform crate;
    [SerializeField] private Ease ease;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnNumberFall,OnNumberFall);

    }


    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnNumberFall,OnNumberFall);

    }


    private void OnNumberFall()
    {
        inFruitParticle.Play();
        crate.DOScale(Vector3.one*1.2f,duration).OnComplete(()=>crate.DOScale(Vector3.one,duration)).SetEase(ease);
    }
}
