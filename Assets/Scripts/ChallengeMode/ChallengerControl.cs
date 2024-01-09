using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengerControl : MonoBehaviour
{
    //1 tane farkli event sec. Onu firlattiginda yeni sayiya gecsin. Simdilik deneme icin ayni eventi kullanicam

    [SerializeField] private List<GameObject> numbers=new List<GameObject>();

    //Bunu daha sonra leveldata ile bagla ki oyuncu ciktiginda girdiginde aynı yerden devam etsin. Ya da yaninca en basa atmasin.
    [SerializeField] private LevelData levelData;

    




    private void OnStartChallengeMode()
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            numbers[i].SetActive(false);
        }

        numbers[levelData.challengeIndex].SetActive(true);
        
    }
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnMatchChallengeNumber,OnMatchChallengeNumber);
        EventManager.AddHandler(GameEvent.OnStartChallengeMode,OnStartChallengeMode);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnMatchChallengeNumber,OnMatchChallengeNumber);
        EventManager.RemoveHandler(GameEvent.OnStartChallengeMode,OnStartChallengeMode);
    }

    private void OnMatchChallengeNumber()
    {
        levelData.challengeIndex++;
        for (int i = 0; i < numbers.Count; i++)
        {
            numbers[i].SetActive(false);
        }

        numbers[levelData.challengeIndex].SetActive(true);
        EventManager.Broadcast(GameEvent.OnUpdateChallenger);

        //Sona ulasirsa odul.
    }


}
