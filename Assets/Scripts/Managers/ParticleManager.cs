using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> successParticles=new List<ParticleSystem>();
    [SerializeField] private List<ParticleSystem> hitParticles=new List<ParticleSystem>();

    private int index;

    [SerializeField] private GameData gameData;
    [SerializeField] private ParticleSystem mouthParticle;
    [SerializeField] private ParticleSystem sizeUpParticle;

    private WaitForSeconds waitForSeconds;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnPlayerEat,OnPlayerEat);
        EventManager.AddHandler(GameEvent.OnPlayerSizeUp,OnPlayerSizeUp);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnPlayerEat,OnPlayerEat);
        EventManager.AddHandler(GameEvent.OnPlayerSizeUp,OnPlayerSizeUp);

    }

    private void Start()
    {
        waitForSeconds=new WaitForSeconds(.5f);
    }

    private void OnPlayerSizeUp()
    {
        sizeUpParticle.Play();
    }

    private void OnSuccess()
    {
        for (int i = 0; i < successParticles.Count; i++)
        {
            successParticles[i].Play();
        }
    }

    private void OnPlayerEat()
    {
        mouthParticle.Play();
    }

    
}
