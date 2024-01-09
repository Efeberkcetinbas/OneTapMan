using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Openings : MonoBehaviour
{
    [SerializeField] private List<GameObject> cubes=new List<GameObject>();

    [SerializeField] private Ease ease;

    [SerializeField] private float duration;

    private WaitForSeconds waitForSeconds;
    [SerializeField] private GameData gameData;
    private void Start()
    {
        waitForSeconds=new WaitForSeconds(.2f);
        for (int i = 0; i < cubes.Count; i++)
        {
            cubes[i].SetActive(false);
        }
    }

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
        if(!gameData.isChallengerLevel)
            StartCoroutine(StartCubes());
    }

    private IEnumerator StartCubes()
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            cubes[i].SetActive(true);
            cubes[i].transform.localScale=Vector3.zero;
            cubes[i].transform.DOScale(Vector3.one,duration);
            yield return waitForSeconds;

        }
    }
}
