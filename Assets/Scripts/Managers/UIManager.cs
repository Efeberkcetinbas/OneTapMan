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

    [Header("INCREMENTALS")]
    [SerializeField] private TextMeshProUGUI increaseScoreIncrementalText;
    [SerializeField] private TextMeshProUGUI pressTimeIncrementalText;
    [SerializeField] private TextMeshProUGUI priceIncreaseScoreIncrementalText;
    [SerializeField] private TextMeshProUGUI pricePressTimeIncrementalText;
    [SerializeField] private TextMeshProUGUI incrementalScoreText;


    [Header("Settings")]
    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject soundOff;
    private bool isSoundOn=true;

    [Header("Challenger Level")]
    [SerializeField] private TextMeshProUGUI stateNumberText;



    
    
    [Header("Data's")]
    public GameData gameData;
    public PlayerData playerData;
    public LevelData levelData;
    public IncrementalData incrementalData;

    

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnUIRequirementUpdate,OnUIRequirementUpdate);
        
        EventManager.AddHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);

        //incremental
        EventManager.AddHandler(GameEvent.OnUpdateIncreaseScore,OnUpdateIncreaseScore);
        EventManager.AddHandler(GameEvent.OnUpdatePressTime,OnUpdatePressTime);
        
        //settings
        EventManager.AddHandler(GameEvent.OnAudioOffOn,OnAudioOffOn);

        //challenger
        EventManager.AddHandler(GameEvent.OnUpdateChallenger,OnUpdateChallenger);

    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnUIRequirementUpdate,OnUIRequirementUpdate);

        EventManager.RemoveHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);

        //incremental
        EventManager.RemoveHandler(GameEvent.OnUpdateIncreaseScore,OnUpdateIncreaseScore);
        EventManager.RemoveHandler(GameEvent.OnUpdatePressTime,OnUpdatePressTime);
        
        //settings
        EventManager.RemoveHandler(GameEvent.OnAudioOffOn,OnAudioOffOn);

        //challenger
        EventManager.RemoveHandler(GameEvent.OnUpdateChallenger,OnUpdateChallenger);

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

    #region Incrementals
    
    private void OnUpdateIncreaseScore()
    {
        increaseScoreIncrementalText.SetText("+ " +  incrementalData.increaseScore.ToString());
        priceIncreaseScoreIncrementalText.SetText(incrementalData.IncreaseScorePrice.ToString());
        incrementalScoreText.SetText(levelData.score.ToString());
        
    }

    private void OnUpdatePressTime()
    {
        pressTimeIncrementalText.SetText("+ " +  incrementalData.pressTime.ToString());
        pricePressTimeIncrementalText.SetText(incrementalData.PressTimePrice.ToString());
        incrementalScoreText.SetText(levelData.score.ToString());

    }
    #endregion

    

    private void OnAudioOffOn()
    {
        isSoundOn=!isSoundOn;

        if(isSoundOn)
            SoundOnOff(false,true,soundOn,soundOff);
        else
            SoundOnOff(true,false,soundOn,soundOff);

    }

    private void SoundOnOff(bool val1,bool val2,GameObject gameObject1,GameObject gameObject2)
    {
        gameObject1.SetActive(val1);
        gameObject2.SetActive(val2);
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

    private void OnUpdateChallenger()
    {
        stateNumberText.SetText((levelData.challengeIndex+1).ToString() + " / 40 ");
    }
    private void OnShopBallSelected()
    {
        shoppingScore.SetText(levelData.score.ToString());
    }


    
}
