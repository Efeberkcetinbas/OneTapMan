using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PanelManager : MonoBehaviour
{
    [SerializeField] private RectTransform StartPanel,CharacterPanel,IncrementalPanel,SuccessPanel,FailPanel,ScoreImage;

    [SerializeField] private GameObject[] sceneUI;
    [SerializeField] private Image Fade;

    [SerializeField] private float StartX,StartY,CharacterX,CharacterY,IncrementalX,IncrementalY,ScoreX,ScoreOldX,duration;

    [SerializeField] private GameData gameData;
    [SerializeField] private PlayerData playerData;
    [Header("Game Ending List")]
    [SerializeField] private Ease ease;
    [SerializeField] private List<Transform> successElements=new List<Transform>();
    [SerializeField] private List<Image> stars=new List<Image>();
    [SerializeField] private List<Transform> failElements=new List<Transform>();

    //Waitforseconds
    private WaitForSeconds waitForSeconds1;
    private WaitForSeconds waitForSeconds2;
    private WaitForSeconds waitForSecondsScore;


    [Header("Settings")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject settingsButton;



    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);

    }


    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
    }

    private void Start() 
    {
        SceneUI(false);

        waitForSeconds1=new WaitForSeconds(2);
        waitForSeconds2=new WaitForSeconds(.5f);
        waitForSecondsScore=new WaitForSeconds(2);
    }

    

    

    
    
    public void StartGame() 
    {
        //EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>{
            SceneUI(true);
            /*ScoreImage.DOAnchorPosX(ScoreOldX,0.5f);
            StartCoroutine(ScoreMove());*/
            //player.transform.DOMoveY(0.5f,0.5f).OnComplete(()=>playerData.playerCanMove=true);
            EventManager.Broadcast(GameEvent.OnStartGame);
            gameData.isGameEnd=false;
           

            //StartPanel.gameObject.SetActive(false);
        });
    }

    #region Settings
    public void OpenSettingsPanel()
    {
        gameData.isGameEnd=true;
        settingsPanel.SetActive(true);
        settingsButton.SetActive(false);
        settingsPanel.transform.localScale=Vector3.zero;
        settingsPanel.transform.DOScale(Vector3.one,0.5f).SetEase(Ease.InOutElastic);
    }

    public void OnAudioOffOn()
    {
        EventManager.Broadcast(GameEvent.OnAudioOffOn);
    }

    public void CloseSettingsPanel()
    {
        gameData.isGameEnd=false;
        settingsPanel.transform.DOScale(Vector3.zero,0.5f).SetEase(Ease.InCubic).OnComplete(()=>{
            settingsPanel.SetActive(false);
            settingsButton.SetActive(true);
        });
    
    }


    #endregion
    

    
 

    private void OnRestartLevel()
    {
        FailPanel.DOAnchorPos(new Vector2(2500,0),0.1f).OnComplete(()=>{
            for (int i = 0; i < failElements.Count; i++)
            {
                failElements[i].transform.localScale=Vector3.zero;
                
            }
            FailPanel.gameObject.SetActive(false);
         });
        SceneUI(true);
        
        
      
    }

    private void OnNextLevel()
    {
       
        SuccessPanel.DOAnchorPos(new Vector2(2500,0),0.1f).OnComplete(()=>{
            
            for (int i = 0; i < successElements.Count; i++)
            {
                successElements[i].transform.localScale=Vector3.zero;
                
            }
            SuccessPanel.gameObject.SetActive(false);
        });
        
        StartPanel.gameObject.SetActive(true);
        StartPanel.transform.localScale=Vector3.one;
        StartPanel.DOAnchorPos(Vector2.zero,0.1f);

        StartCoroutine(Blink(Fade.gameObject,Fade));
        for (int i = 0; i < sceneUI.Length; i++)
        {
            sceneUI[i].SetActive(false);
        }

    }

   

    

    private void SceneUI(bool val)
    {
        for (int i = 0; i < sceneUI.Length; i++)
        {
            sceneUI[i].SetActive(val);
        }
    }

    private void OnOpenSuccess()
    {
       
        SceneUI(false);
        SuccessPanel.gameObject.SetActive(true);
        SuccessPanel.DOAnchorPos(Vector2.zero,0.2f).SetEase(Ease.InOutCubic).OnComplete(()=>{
            StartCoroutine(ItemsAnimation(successElements));
            StartCoroutine(ItemsFillAnimation(stars));
            
        });
    }

    private void OnOpenFail()
    {
        
        SceneUI(false);
        FailPanel.gameObject.SetActive(true);
        FailPanel.DOAnchorPos(Vector2.zero,0.2f).SetEase(Ease.InOutCubic).OnComplete(()=>{
            StartCoroutine(ItemsAnimation(failElements));
        });
    }
  

    private IEnumerator Blink(GameObject gameObject,Image image)
    {
        
        gameObject.SetActive(true);
        image.color=new Color(0,0,0,1);
        image.DOFade(0,2f);
        yield return waitForSeconds1;
        gameObject.SetActive(false);
    }

    private IEnumerator ScoreMove()
    {
        yield return waitForSecondsScore;
        ScoreImage.DOAnchorPosX(ScoreX,0.5f);
    }

    private IEnumerator ItemsAnimation(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].transform.localScale=Vector3.zero;
        }

        for (int i = 0; i < list.Count; i++)
        {
            list[i].transform.DOScale(1f,1f).SetEase(ease);
            yield return waitForSeconds2;
        }
    }

    private IEnumerator ItemsFillAnimation(List<Image> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].fillAmount=0;
        }

        for (int i = 0; i < list.Count; i++)
        {
            yield return waitForSeconds2;
            list[i].DOFillAmount(1,.5f);
        }
    }

  

    public void OpenCharacterPanel()
    {
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        CharacterPanel.gameObject.SetActive(true);
        CharacterPanel.DOAnchorPos(new Vector2(0,0),duration);
        EventManager.Broadcast(GameEvent.OnShopOpen);
    }

    public void OpenIncrementalPanel()
    {
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        IncrementalPanel.gameObject.SetActive(true);
        IncrementalPanel.DOAnchorPos(new Vector2(0,0),duration);
        EventManager.Broadcast(GameEvent.OnIncrementalOpen);
    }

    public void BackToStart(bool isOnCharacter)
    {
        if(isOnCharacter)
        {
            StartPanel.gameObject.SetActive(true);
            StartPanel.DOAnchorPos(Vector2.zero,duration);
            CharacterPanel.DOAnchorPos(new Vector2(CharacterX,CharacterY),duration);
            EventManager.Broadcast(GameEvent.OnShopClose);
        }
        else
        {
            StartPanel.gameObject.SetActive(true);
            StartPanel.DOAnchorPos(Vector2.zero,duration);
            IncrementalPanel.DOAnchorPos(new Vector2(IncrementalX,IncrementalY),duration);
            EventManager.Broadcast(GameEvent.OnShopClose);
        }
    }
}
