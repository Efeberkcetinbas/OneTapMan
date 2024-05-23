using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using  DG.Tweening;
public class OurBowl : MonoBehaviour, IBowl
{   


    [Header("DATA")]
    [SerializeField] private GameData gameData;


    [Header("XP Effect")]
    [SerializeField] private GameObject scoreXP;
    [SerializeField] private Vector3 minBounds;
    [SerializeField] private Vector3 maxBounds;
    //Camera
    private Camera cam;

    private void Start()
    {
        cam=Camera.main;
    }


    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);

    }

    private void OnStopTimer()
    {
        XPEffect();
        CalculateWeight();
    }
    public void CalculateWeight()
    {
        gameData.totalWeightOurBowl+=gameData.RoundedTime;
        EventManager.Broadcast(GameEvent.OnUpdateOurWeightUI);
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

    


}
