using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("Scriptable Data's")]
    public GameData gameData;
    public PlayerData playerData;


    [Header("Open/Close")]
    [SerializeField] private GameObject[] open_close;
    
    //Boss Ball

    private WaitForSeconds waitForSeconds;

    [SerializeField]private int hitNumber;

    private void Awake() 
    {
        ClearData();
    }

    
    
    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(3);
        UpdateRequirement();
    }


    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);
        EventManager.AddHandler(GameEvent.OnMatchNumber,OnMatchNumber);
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);

    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);
        EventManager.RemoveHandler(GameEvent.OnMatchNumber,OnMatchNumber);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);

    }

   
    private void UpdateRequirement()
    {
        //FindObjectOfType
        gameData.ReqMove=FindObjectOfType<LevelRequirement>().MoveNumber;
        gameData.NeededNumber=FindObjectOfType<LevelNeededCube>().NeededNumber;
        EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);
    }

    private void OnStopTimer()
    {
        //gameData.ReqMove
        gameData.ReqMove--;
        EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);

        if(gameData.ReqMove>0)
            return;
        else
            StartCoroutine(CheckIfGameEnds());
    }

    private void OnMatchNumber()
    {
        hitNumber++;
        if(hitNumber==gameData.NeededNumber)
        {
            gameData.isGameEnd=true;
            StartCoroutine(StartSuccess());
        }
            //Debug.Log("SUCCESS");
        else
            return;
    }

    private IEnumerator StartSuccess()
    {
        yield return waitForSeconds;
        EventManager.Broadcast(GameEvent.OnSuccess);
    }

    private IEnumerator CheckIfGameEnds()
    {
        yield return waitForSeconds;
        gameData.isGameEnd=true;
        Debug.Log("HERE CHECK IF GAME END");
        if(hitNumber==gameData.NeededNumber)
            EventManager.Broadcast(GameEvent.OnSuccess);
//            Debug.Log("SUCCESS");
        else
            Debug.Log("FAIL");
    }

    private void OnSuccess()
    {
        gameData.isGameEnd=true;
        StartCoroutine(OpenSuccessPanel());
    }

    private IEnumerator OpenSuccessPanel()
    {
        yield return waitForSeconds;
        EventManager.Broadcast(GameEvent.OnOpenSuccess);
    }
    


    private void OnNextLevel()
    {
        ClearData();
        UpdateRequirement();
    }

    private void OnRestartLevel()
    {
        UpdateRequirement();
    }
    
    
    void ClearData()
    {
        gameData.isGameEnd=true;
        playerData.playerCanMove=true;


        hitNumber=0;
   
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
