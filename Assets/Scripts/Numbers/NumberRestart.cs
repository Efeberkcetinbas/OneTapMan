using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberRestart : MonoBehaviour
{
    [SerializeField] private List<DestroyNumber> numbers=new List<DestroyNumber>();



    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
    }


    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
    }


    private void OnRestartLevel()
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            numbers[i].gameObject.SetActive(true);
            numbers[i].OnRestartLevel();
        }
    }
}
