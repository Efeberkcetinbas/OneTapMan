using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip successSound,openSuccessSound,HitNumberSound,StopSound;

    AudioSource musicSource,effectSource;

    private bool hit;

    private void Start() 
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = GameLoop;
        //musicSource.Play();
        effectSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnHitNumbers,OnHitNumbers);
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);
    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnHitNumbers,OnHitNumbers);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);
        
    }

   

    

    private void OnHitNumbers()
    {
        effectSource.PlayOneShot(HitNumberSound);
    }

    

    private void OnSuccess()
    {
        effectSource.PlayOneShot(successSound);
    }

    private void OnOpenSuccess()
    {
        effectSource.PlayOneShot(openSuccessSound);
    }

    private void OnStopTimer()
    {
        effectSource.PlayOneShot(StopSound);
    }   

}
