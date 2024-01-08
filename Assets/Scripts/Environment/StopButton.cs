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

    [SerializeField] private Color startColor,stopColor;

    private void Start()
    {
        //OnNextLevel();
    }
    
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStartTimer,OnStartTimer);
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStartTimer,OnStartTimer);
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnStartTimer()
    {
        HitButton(startColor);
    }

    private void OnStopTimer()
    {
        HitButton(stopColor);
    }
    private void OnNextLevel()
    {
        HitButton(stopColor);
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
