using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BuffManager : MonoBehaviour
{
    //Invincle, Destroyer

    
    public GameData gameData;
    public PlayerData playerData;

    [SerializeField] private bool isBuffused=false;

    [Header("Buff Values / Boolean")]
    [SerializeField] private bool isInfinityOpen;
    [SerializeField] private bool isPassionVictoryOpen;
    [SerializeField] private bool isOnePunchManOpen;

    [Header("Buff Values / Buttons")]
    [SerializeField] private Button infinityButton;
    [SerializeField] private Button passionVictoryButton;
    [SerializeField] private Button onePunchManButton;

    

    [Header("Buff Values / Texts")]
    [SerializeField] private TextMeshProUGUI infinityText;
    [SerializeField] private TextMeshProUGUI passionVictoryText;
    [SerializeField] private TextMeshProUGUI onePunchManText;



    [SerializeField] private GameObject buffBar;

    private void Start()
    {
        OnOpenBuffPanel();
    }
    
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnStartGame,OnGameStart);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnStartGame,OnGameStart);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnNextLevel()
    {
        isBuffused=false;
    }

    private void OnOpenBuffPanel()
    {
        SetPriceTexts(infinityText,playerData.PriceValInfinity);
        SetPriceTexts(passionVictoryText,playerData.PriceValPassionVictory);
        SetPriceTexts(onePunchManText,playerData.PriceValOnePunchMan);

        CheckButtons(playerData.PriceValInfinity,infinityButton);
        CheckButtons(playerData.PriceValPassionVictory,passionVictoryButton);
        CheckButtons(playerData.PriceValOnePunchMan,onePunchManButton);
    }

    #region BuffUIControl
    private void SetPriceTexts(TextMeshProUGUI textMeshProUGUI,int val)
    {
        textMeshProUGUI.SetText(val.ToString());
    }

    private void CheckButtons(int val,Button button)
    {
        if(val<=gameData.XP)
        {
            button.interactable=true;
        }
        else
        {

            button.interactable=false;
        }
    }

    private void BuyBuff(bool val,int priceVal,Button button,TextMeshProUGUI textMeshProUGUI)
    {
        if(gameData.XP>=priceVal)
        {
            val=true;
            isBuffused=true;
            gameData.XP-=priceVal;
            playerData.SaveData();
        }

        else
        {
            return;
        }
    }

    #endregion

    #region Buffs Panel

    public void SetPassionVictory()
    {
        BuyBuff(isPassionVictoryOpen,playerData.PriceValPassionVictory,passionVictoryButton,passionVictoryText);
        isPassionVictoryOpen=true;
        playerData.PriceValPassionVictory*=2;
        OnOpenBuffPanel();
    }
    public void SetInfinity()
    {
        BuyBuff(isInfinityOpen,playerData.PriceValInfinity,infinityButton,infinityText);
        isInfinityOpen=true;
        playerData.PriceValInfinity*=2;
        SetPriceTexts(infinityText, playerData.PriceValInfinity);
        OnOpenBuffPanel();
    }
    public void SetOnePunchMan()
    {
        BuyBuff(isOnePunchManOpen,playerData.PriceValOnePunchMan,onePunchManButton,onePunchManText);
        isOnePunchManOpen=true;
        playerData.PriceValOnePunchMan*=2;
        SetPriceTexts(onePunchManText, playerData.PriceValOnePunchMan);
        OnOpenBuffPanel();
    }

    
    

    #endregion 

    
    #region Buffs
    private void DoInfinity()
    {
        isBuffused=true;

        //Ekonomi lazim. Buff aldigimizda ucret artmali. 
        //Buff'in baslamasini game is Start eventine bagla
    }

    private void DoPassionVictory()
    {
        isBuffused=true;
    }

    private void DoOnePunchMan()
    {
        isBuffused=true;
    }

    #endregion

    private void OnGameStart()
    {
        if(isBuffused)
        {
            if(isPassionVictoryOpen) DoPassionVictory();
            if(isOnePunchManOpen) DoInfinity();
            if(isInfinityOpen) DoOnePunchMan();
            
            buffBar.SetActive(true);
            //EventManager.Broadcast(GameEvent.OnUpdateBuff);
        }
    }

    



    #region Caroutines
    //Sure baslangicini iyi ayarlamak lazim
    private IEnumerator DoReverse(GameEvent gameEvent)
    {
        yield return new WaitForSeconds(gameData.BuffTime);
        EventManager.Broadcast(gameEvent);
    }
    #endregion
}
