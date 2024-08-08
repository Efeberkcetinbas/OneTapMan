using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class PlayerWeight : MonoBehaviour
{   
    [SerializeField] private GameData gameData;
    
    public TextMeshPro currentWeightText;

    [SerializeField] private ParticleSystem spawnParticle;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPlayerEat,OnPlayerEat);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPlayerEat,OnPlayerEat);

    }

    private void OnPlayerEat()
    {
        DOTween.To(GetWeight,ChangeWeight,gameData.totalWeightOurBowl+gameData.RoundedTime,0.25f).OnUpdate(UpdateWeightUI);
        spawnParticle.Play();
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
}
