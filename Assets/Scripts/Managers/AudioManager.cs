using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip successSound,openSuccessSound,StopSound,OpeningSound,NextLevelSound,EatSound,SizeUpSound,FailSound,DeadSound,BallSound,
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
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnShopOpen,OnShopOpen);
        EventManager.AddHandler(GameEvent.OnShopClose,OnShopClose);
        EventManager.AddHandler(GameEvent.OnStartGame,OnStartGame);
        EventManager.AddHandler(GameEvent.OnIncrementalOpen,OnIncrementalOpen);
        EventManager.AddHandler(GameEvent.OnAudioOffOn,OnAudioOffOn);
        EventManager.AddHandler(GameEvent.OnPlayerEat,OnPlayerEat);
        EventManager.AddHandler(GameEvent.OnPlayerSizeUp,OnPlayerSizeUp);
        EventManager.AddHandler(GameEvent.OnFail,OnFail);
        EventManager.AddHandler(GameEvent.OnOpenFail,OnOpenFail);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnStartGame);
        EventManager.AddHandler(GameEvent.OnSpawnBall,OnSpawnBall);

    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnShopOpen,OnShopOpen);
        EventManager.RemoveHandler(GameEvent.OnShopClose,OnShopClose);
        EventManager.RemoveHandler(GameEvent.OnStartGame,OnStartGame);
        EventManager.RemoveHandler(GameEvent.OnIncrementalOpen,OnIncrementalOpen);
        EventManager.RemoveHandler(GameEvent.OnAudioOffOn,OnAudioOffOn);
        EventManager.RemoveHandler(GameEvent.OnPlayerEat,OnPlayerEat);
        EventManager.RemoveHandler(GameEvent.OnPlayerSizeUp,OnPlayerSizeUp);
        EventManager.RemoveHandler(GameEvent.OnFail,OnFail);
        EventManager.RemoveHandler(GameEvent.OnOpenFail,OnOpenFail);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnStartGame);
        EventManager.RemoveHandler(GameEvent.OnSpawnBall,OnSpawnBall);
        
    }

   

    

   
    private void OnSuccess()
    {
        effectSource.PlayOneShot(successSound);
    }

    private void OnOpenSuccess()
    {
        effectSource.PlayOneShot(openSuccessSound);
    }

   

    private void OnFail()
    {
        effectSource.PlayOneShot(DeadSound);
    }

    private void OnOpenFail()
    {
        effectSource.PlayOneShot(FailSound);
    }

    private void OnSpawnBall()
    {
        effectSource.PlayOneShot(BallSound);
        Debug.Log("SĞAAAA");
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

    private void OnPlayerEat()
    {
        effectSource.PlayOneShot(EatSound);
    }

    private void OnPlayerSizeUp()
    {
        effectSource.PlayOneShot(SizeUpSound);
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
