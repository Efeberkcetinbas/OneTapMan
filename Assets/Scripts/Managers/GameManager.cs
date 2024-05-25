using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("Scriptable Data's")]
    public GameData gameData;
    public PlayerData playerData;
    public LevelData levelData;


    [Header("Open/Close")]
    [SerializeField] private GameObject[] open_close;
    

    private WaitForSeconds waitForSeconds;
    private WaitForSeconds waitForSecondsForPanel;
    
    private WaitForSeconds waitForSecondsCheck;


    private void Awake() 
    {
        ClearData();
    }

    
    
    private void Start() 
    {
        waitForSecondsCheck=new WaitForSeconds(1);
        waitForSeconds=new WaitForSeconds(0.75f);
        waitForSecondsForPanel=new WaitForSeconds(1);
        UpdateRequirement();
    }


    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnFail,OnFail);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.AddHandler(GameEvent.OnMatchNumber,OnMatchNumber);
        EventManager.AddHandler(GameEvent.OnDisMatchNumber,OnDisMatchNumber);

    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnFail,OnFail);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.RemoveHandler(GameEvent.OnMatchNumber,OnMatchNumber);
        EventManager.RemoveHandler(GameEvent.OnDisMatchNumber,OnDisMatchNumber);

    }

   
    private void UpdateRequirement()
    {
        //FindObjectOfType
        gameData.ReqMove=FindObjectOfType<LevelRequirement>().MoveNumber;
        //gameData.NeededNumber=FindObjectOfType<LevelNeededCube>().NeededNumber;
        EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);
    }

    private void OnStopTimer()
    {
        gameData.ReqMove--;
        EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);

        if(gameData.ReqMove>0)
            return;
        else
        {
            gameData.isGameEnd=true;
            StartCoroutine(CallCheckZeroEvent());
        }
            
    }

    private IEnumerator CallCheckZeroEvent()
    {
        yield return waitForSecondsCheck;
        EventManager.Broadcast(GameEvent.OnCheckZero);
    }

    

    private void OnSuccess()
    {
        gameData.isGameEnd=true;
        StartCoroutine(OpenSuccessPanel());
    }

    private void OnFail()
    {
        gameData.isGameEnd=true;
        StartCoroutine(OpenFailPanel());
    }

    private IEnumerator OpenSuccessPanel()
    {
        yield return waitForSecondsForPanel;
        EventManager.Broadcast(GameEvent.OnOpenSuccess);
    }

    private IEnumerator OpenFailPanel()
    {
        yield return waitForSecondsForPanel;
        EventManager.Broadcast(GameEvent.OnOpenFail);
    }

    private void OnMatchNumber()
    {
        StartCoroutine(CallSuccess());
    }

    private void OnDisMatchNumber()
    {
        StartCoroutine(CallFail());
    }

    private IEnumerator CallSuccess()
    {
        yield return waitForSeconds;
        EventManager.Broadcast(GameEvent.OnSuccess);

    }

    private IEnumerator CallFail()
    {
        yield return waitForSeconds;
        EventManager.Broadcast(GameEvent.OnFail);
    }

    
   

    private void OnNextLevel()
    {
        ClearData();
        UpdateRequirement();
        EventManager.Broadcast(GameEvent.OnUpdateOurWeightUI);

    }

    private void OnRestartLevel()
    {
        gameData.isGameEnd=false;
        UpdateRequirement();
        gameData.totalWeightOurBowl=0;
        EventManager.Broadcast(GameEvent.OnUpdateOurWeightUI);

    }

   
    
    
    void ClearData()
    {
        gameData.isGameEnd=true;
        playerData.playerCanMove=true;
        
        gameData.totalWeightOurBowl=0;
   
    }


    public void OpenClose(GameObject[] gameObjects,bool canOpen)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if(canOpen)
                gameObjects[i].SetActive(true);
            else
                gameObjects[i].SetActive(false);
        }
    }

    
}
