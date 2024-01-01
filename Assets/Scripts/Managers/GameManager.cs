using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("Scriptable Data's")]
    public GameData gameData;
    public PlayerData playerData;

    [Header("Player")]
    [SerializeField] private Transform player;
    //Level Progress

   
    public GameObject failPanel;
    [SerializeField] private Ease ease;


    [Header("Open/Close")]
    [SerializeField] private GameObject[] open_close;
    
    //Boss Ball

    private WaitForSeconds waitForSeconds;

    

    private void Awake() 
    {
        ClearData();
    }

    
    
    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(1);
        UpdateRequirement();
    }


    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);

    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);

    }

   
    private void UpdateRequirement()
    {
        //FindObjectOfType
        gameData.tempMove=0;
        gameData.ReqMove=FindObjectOfType<LevelRequirement>().MoveNumber;
        EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);
    }

    private void OnSuccess()
    {
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
        failPanel.SetActive(false);
        
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
