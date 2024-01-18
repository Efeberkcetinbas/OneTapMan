using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip successSound,openSuccessSound,HitNumberSound,StopSound,FallSound,OpeningSound,NextLevelSound,
    UISound,throwSound,swordHitSound;

    AudioSource musicSource,effectSource;

    private bool isSoundOn=true;

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
        EventManager.AddHandler(GameEvent.OnStartTimer,OnStartTimer);
        EventManager.AddHandler(GameEvent.OnNumberFall,OnNumberFall);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnShopOpen,OnShopOpen);
        EventManager.AddHandler(GameEvent.OnShopClose,OnShopClose);
        EventManager.AddHandler(GameEvent.OnStartGame,OnStartGame);
        EventManager.AddHandler(GameEvent.OnIncrementalOpen,OnIncrementalOpen);
        EventManager.AddHandler(GameEvent.OnAudioOffOn,OnAudioOffOn);
        EventManager.AddHandler(GameEvent.OnThrowSword,OnThrowSword);
        EventManager.AddHandler(GameEvent.OnHitSword,OnHitSword);

    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnHitNumbers,OnHitNumbers);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);
        EventManager.RemoveHandler(GameEvent.OnStartTimer,OnStartTimer);
        EventManager.RemoveHandler(GameEvent.OnNumberFall,OnNumberFall);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnShopOpen,OnShopOpen);
        EventManager.RemoveHandler(GameEvent.OnShopClose,OnShopClose);
        EventManager.RemoveHandler(GameEvent.OnStartGame,OnStartGame);
        EventManager.RemoveHandler(GameEvent.OnIncrementalOpen,OnIncrementalOpen);
        EventManager.RemoveHandler(GameEvent.OnAudioOffOn,OnAudioOffOn);
        EventManager.RemoveHandler(GameEvent.OnThrowSword,OnThrowSword);
        EventManager.RemoveHandler(GameEvent.OnHitSword,OnHitSword);
        
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

    private void OnStartTimer()
    {
        effectSource.PlayOneShot(StopSound);
    }


    private void OnNumberFall()
    {
        effectSource.PlayOneShot(FallSound);
    }

    private void OnNextLevel()
    {
        effectSource.PlayOneShot(NextLevelSound);
    }

    private void OnStartGame()
    {
        effectSource.PlayOneShot(OpeningSound);
    }

    private void OnShopOpen()
    {
        effectSource.PlayOneShot(UISound);
    }

    private void OnShopClose()
    {
        effectSource.PlayOneShot(UISound);
    }

    private void OnIncrementalOpen()
    {
        effectSource.PlayOneShot(UISound);
    }

    private void OnThrowSword()
    {
        effectSource.PlayOneShot(throwSound);
    }

    private void OnHitSword()
    {
        effectSource.PlayOneShot(swordHitSound);
    }

    private void OnAudioOffOn()
    {
        isSoundOn=!isSoundOn;

        if(isSoundOn)
            effectSource.mute=true;
        else
            effectSource.mute=false;
    }

    



}
