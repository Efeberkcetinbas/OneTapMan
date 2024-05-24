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
    private WaitForSeconds waitForSecondsFail;

    [SerializeField]private int hitNumber;

    private void Awake() 
    {
        ClearData();
    }

    
    
    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(1.5f);
        waitForSecondsFail=new WaitForSeconds(.5f);
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

    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnFail,OnFail);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.RemoveHandler(GameEvent.OnMatchNumber,OnMatchNumber);

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
            EventManager.Broadcast(GameEvent.OnCheckZero);
        }
            
    }

    

    private IEnumerator StartSuccess()
    {
        yield return waitForSeconds;
        EventManager.Broadcast(GameEvent.OnSuccess);
    }

    private IEnumerator StartFail()
    {
        yield return waitForSecondsFail;
        EventManager.Broadcast(GameEvent.OnFail);
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
        yield return waitForSeconds;
        EventManager.Broadcast(GameEvent.OnOpenSuccess);
    }

    private IEnumerator OpenFailPanel()
    {
        yield return waitForSeconds;
        EventManager.Broadcast(GameEvent.OnOpenFail);
    }

    private void OnMatchNumber()
    {
        Debug.Log("SUCCESSSSSSSS");
        StartCoroutine(OpenSuccessPanel());
    }

    
   

    private void OnNextLevel()
    {
        ClearData();
        UpdateRequirement();
    }

    private void OnRestartLevel()
    {
        gameData.isGameEnd=false;
        hitNumber=0;
        UpdateRequirement();
    }

   
    
    
    void ClearData()
    {
        gameData.isGameEnd=true;
        playerData.playerCanMove=true;
        
        hitNumber=0;
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
