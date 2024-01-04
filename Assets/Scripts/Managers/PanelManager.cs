using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PanelManager : MonoBehaviour
{
    [SerializeField] private RectTransform StartPanel,StorePanel,SuccessPanel,FailPanel,ScoreImage;

    [SerializeField] private GameObject[] sceneUI;
    [SerializeField] private Image Fade;

    [SerializeField] private float StartX,StartY,StoreX,StoreY,ScoreX,ScoreOldX,duration;

    [SerializeField] private GameData gameData;
    [SerializeField] private PlayerData playerData;
    [Header("Success List")]
    [SerializeField] private Ease ease;
    [SerializeField] private List<Transform> successElements=new List<Transform>();
    [SerializeField] private List<Transform> failElements=new List<Transform>();

    //Waitforseconds
    private WaitForSeconds waitForSeconds1;
    private WaitForSeconds waitForSeconds2;
    private WaitForSeconds waitForSecondsScore;



    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);

    }


    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
    }

    private void Start() 
    {
        SceneUI(false);

        waitForSeconds1=new WaitForSeconds(2);
        waitForSeconds2=new WaitForSeconds(.5f);
        waitForSecondsScore=new WaitForSeconds(2);
    }

    private void OnSuccess()
    {
    }

    

    
    
    public void StartGame() 
    {
        //EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>{
            SceneUI(true);
            ScoreImage.DOAnchorPosX(ScoreOldX,0.5f);
            StartCoroutine(ScoreMove());
            //player.transform.DOMoveY(0.5f,0.5f).OnComplete(()=>playerData.playerCanMove=true);
            gameData.isGameEnd=false;
            
           

            //StartPanel.gameObject.SetActive(false);
        });
    }

    

    
 

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
        StartPanel.DOAnchorPos(Vector2.zero,0.1f).OnComplete(()=>EventManager.Broadcast(GameEvent.OnIncreaseScore));

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

  

    public void OpenStorePanel()
    {
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        StorePanel.gameObject.SetActive(true);
        StorePanel.DOAnchorPos(new Vector2(0,750),duration);
        EventManager.Broadcast(GameEvent.OnShopOpen);
    }

    public void BackToStart()
    {

        StartPanel.gameObject.SetActive(true);
        StartPanel.DOAnchorPos(Vector2.zero,duration);
        StorePanel.DOAnchorPos(new Vector2(StoreX,StoreY),duration);
        EventManager.Broadcast(GameEvent.OnShopClose);


    }
}
