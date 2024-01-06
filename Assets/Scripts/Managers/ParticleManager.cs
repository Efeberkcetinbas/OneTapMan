using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> successParticles=new List<ParticleSystem>();
    [SerializeField] private List<ParticleSystem> hitParticles=new List<ParticleSystem>();

    private int index;

    [SerializeField] private GameData gameData;

    private WaitForSeconds waitForSeconds;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnHitNumbers,OnHitNumbers);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnHitNumbers,OnHitNumbers);
    }

    private void Start()
    {
        waitForSeconds=new WaitForSeconds(.5f);
    }

    private void OnSuccess()
    {
        for (int i = 0; i < successParticles.Count; i++)
        {
            successParticles[i].Play();
        }
    }

    private void OnHitNumbers()
    {
        index=Random.Range(0,hitParticles.Count);
        hitParticles[index].Play();
    }

    
}
