using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    private bool isStop;

    private float cTime;
    private void Start()
    {
        OnNextLevel();
    }

    private void Update()
    {
        
    }

    private void SetTimer()
    {
        // Start ve Stop Seklinde Yap. Time Football oyununda gibi. Yani ekrana bastigimizda dursun. Tekrar basinca devam.
        // Bu sayede tak tak yapabiliriz. Timer'i gorebiliriz.
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnStartTimer,OnStartTimer);
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);

    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnStartTimer,OnStartTimer);
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);

    }

    private void OnNextLevel()
    {
        gameData.Timer=FindObjectOfType<LevelClockTime>().CurrentTime;
    }

    private void OnStartTimer()
    {   
        isStop=false;
    }

    private void OnStopTimer()
    {
        isStop=true;
    }
}
