using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using  DG.Tweening;
using Unity.Burst.Intrinsics;
public class OurBowl : MonoBehaviour, IBowl
{   


    [Header("DATA")]
    [SerializeField] private GameData gameData;


    [Header("XP Effect")]
    [SerializeField] private GameObject scoreXP;
    [SerializeField] private Vector3 minBounds;
    [SerializeField] private Vector3 maxBounds;

    [Header("WEIGHT")]
    public float scaledWeight = 1.0f;
    public int multiplier;

    private int tempMultip=1;
    //Camera
    private Camera cam;

    private void Start()
    {
        cam=Camera.main;
    }


    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);
        EventManager.AddHandler(GameEvent.OnPlayerEat,OnPlayerEat);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);
        EventManager.RemoveHandler(GameEvent.OnPlayerEat,OnPlayerEat);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);

    }

    private void OnStopTimer()
    {
        //CalculateWeight();
        XPEffect();
    }

    void ScalePlayer(float scale)
    {
        if (transform != null)
        {
            transform.DOScale(new Vector3(scale,scale,scale),0.5f);
        }
    }
    private void OnPlayerEat()
    {
        CalculateWeight();
        CreateScale();
        ScalePlayer(multiplier);
    }

    
    public void CalculateWeight()
    {
        DOTween.To(GetWeight,ChangeWeight,gameData.totalWeightOurBowl+gameData.RoundedTime,0.25f).OnUpdate(UpdateWeightUI);
        /*gameData.totalWeightOurBowl+=gameData.RoundedTime;
        EventManager.Broadcast(GameEvent.OnUpdateOurWeightUI);*/
    }

    private void UpdateWeightUI()
    {
        EventManager.Broadcast(GameEvent.OnUpdateOurWeightUI);
    }

    private void ChangeWeight(int value)
    {
        gameData.totalWeightOurBowl=value;
    }

    private int GetWeight()
    {
        return gameData.totalWeightOurBowl;
    }

     
    private int CreateScale()
    {
        tempMultip=multiplier;
        float clampedTime=Mathf.Clamp(gameData.totalWeightOurBowl+gameData.RoundedTime,100,1000);
        scaledWeight=Mathf.FloorToInt(clampedTime/10)+1;
        multiplier=Mathf.FloorToInt(scaledWeight/10);
        CheckSizeUp();
        return multiplier;
    }

    private void CheckSizeUp()
    {
        if(tempMultip!=multiplier)
            EventManager.Broadcast(GameEvent.OnPlayerSizeUp);
        else
            return;
    }


    internal void XPEffect()
    {
        Vector3 randomPosition=new Vector3(
            Random.Range(minBounds.x,maxBounds.x),
            Random.Range(minBounds.y,maxBounds.y),
            Random.Range(minBounds.z,maxBounds.z)
        );

        GameObject XP=Instantiate(scoreXP,randomPosition,Quaternion.identity);
        XP.transform.LookAt(cam.transform.position);
        XP.transform.DOLocalJump(XP.transform.localPosition,1,1,1,false);
        XP.transform.GetChild(0).GetComponent<TextMeshPro>().text=" + " + gameData.RoundedTime.ToString();
        XP.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>XP.transform.GetChild(0).gameObject.SetActive(false));
        Destroy(XP,2);
    }

    private void OnNextLevel()
    {
        tempMultip=1;
        multiplier=1;
        scaledWeight=1;
        ScalePlayer(1);
        
    }

    private void OnRestartLevel()
    {
        OnNextLevel();
    }
    


}
