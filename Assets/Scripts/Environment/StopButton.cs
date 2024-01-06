using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StopButton : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private float zaxis,oldzAxis,duration;
    [SerializeField] private Transform button;
    
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStartTimer,OnStartTimer);
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStartTimer,OnStartTimer);
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);
    }

    private void OnStartTimer()
    {
        HitButton(Color.green);
    }

    private void OnStopTimer()
    {
        HitButton(Color.red);
    }

    private void HitButton(Color color)
    {
        meshRenderer.material.DOColor(color,duration);
        button.transform.DOLocalMoveZ(zaxis,duration).OnComplete(()=>{
            button.transform.DOLocalMoveZ(oldzAxis,duration);
        });
        particle.Play();
    }
}
