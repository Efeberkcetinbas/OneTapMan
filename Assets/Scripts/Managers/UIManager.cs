using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Text's")]
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI failScore;
    [SerializeField] private TextMeshProUGUI successScore;
    [SerializeField] private TextMeshProUGUI shoppingScore;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI MoveText;
    
    

    private WaitForSeconds waitForSeconds;
    private WaitForSeconds waitForFill;



    
    
    [Header("Data's")]
    public GameData gameData;
    public PlayerData playerData;
    public LevelData levelData;

    

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnUIRequirementUpdate,OnUIRequirementUpdate);
        
        EventManager.AddHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
        
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnUIRequirementUpdate,OnUIRequirementUpdate);

        EventManager.RemoveHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
        
    }
    
    

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(.5f);
        waitForFill=new WaitForSeconds(.5f);
       
        OnNextLevel();
        OnUIUpdate();
    
    }
    
    private void OnUIUpdate()
    {
        score.SetText(levelData.score.ToString());
        score.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.2f).OnComplete(()=>score.transform.DOScale(new Vector3(1,1f,1f),0.2f));
    }

    private void OnNextLevel()
    {
        
        levelText.SetText("LEVEL " + (levelData.IndexOfLevel+1).ToString());
        
    }

    

   



    private void OnUIRequirementUpdate()
    {
        //RequirementNumber
        MoveText.SetText(gameData.ReqMove.ToString());
    }

    private void OnOpenSuccess()
    {
        //successScore.SetText("+ " +  (levelData.score+gameData.increaseScore).ToString());
    }

    private void OnShopBallSelected()
    {
        shoppingScore.SetText(levelData.score.ToString());
    }


    
}
