using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fridge : MonoBehaviour
{
    [SerializeField] private List<GameObject> fridgeList=new List<GameObject>();

    [SerializeField] private float yaxis,duration,oldYaxis;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStartGame,OnStartGame);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStartGame,OnStartGame);
    }


    private void OnStartGame()
    {
        for (int i = 0; i < fridgeList.Count; i++)
        {
            fridgeList[i].transform.DOLocalRotate(new Vector3(0,yaxis,0),duration);
        }
    }

    private void OnRestartLevel()
    {
        for (int i = 0; i < fridgeList.Count; i++)
        {
            fridgeList[i].transform.DOLocalRotate(new Vector3(0,oldYaxis,0),duration);
        }
    }
}
