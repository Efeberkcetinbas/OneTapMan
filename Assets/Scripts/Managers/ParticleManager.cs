using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> successParticles=new List<ParticleSystem>();
    [SerializeField] private List<ParticleSystem> hitParticles=new List<ParticleSystem>();

    private int index;

    [SerializeField] private GameData gameData;
    [SerializeField] private ParticleSystem sizeUpParticle;
    [SerializeField] private ParticleSystem sizeUpParticleLight;

    private WaitForSeconds waitForSeconds;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnPlayerSizeUp,OnPlayerSizeUp);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnPlayerSizeUp,OnPlayerSizeUp);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);

    }

    private void Start()
    {
        waitForSeconds=new WaitForSeconds(.5f);
    }

    private void OnPlayerSizeUp()
    {
        sizeUpParticle.Play();
        sizeUpParticleLight.Play();
    }

    private void OnSuccess()
    {
        SetParticle(true,successParticles);
    }

    

    private void OnNextLevel()
    {
        SetParticle(false,successParticles);
    }

    private void SetParticle(bool val,List<ParticleSystem> particles)
    {
        for (int i = 0; i < particles.Count; i++)
        {
            if(val)
            {
                particles[i].gameObject.SetActive(true);
                particles[i].Play();
            }
                
            else
            {
                particles[i].gameObject.SetActive(false);
                particles[i].Stop();
            }
                
        }
    }

    
}
