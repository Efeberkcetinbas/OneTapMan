using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMesh : MonoBehaviour
{
    [SerializeField] private List<Transform> characters=new List<Transform>();

    [SerializeField] private PlayerData playerData;
    public Transform Mouth;

    [SerializeField] private ParticleSystem destroyParticle;
    [SerializeField] private List<ParticleSystem> mouthParticle=new List<ParticleSystem>();
    [SerializeField] private Color mouthParticleColor;


    


    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnFail,OnFail);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.AddHandler(GameEvent.OnPlayerEat,OnPlayerEat);


    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnFail,OnFail);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.RemoveHandler(GameEvent.OnPlayerEat,OnPlayerEat);

    }


    private void OnFail()
    {
        //index
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].gameObject.SetActive(false);
        }
        destroyParticle.gameObject.SetActive(true);
        destroyParticle.Play();
    }

    private void OnPlayerEat()
    {
        PlayParticles(mouthParticleColor);
    }


    private void OnRestartLevel()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].gameObject.SetActive(false);
        }

        characters[playerData.selectedCharacterIndex].gameObject.SetActive(true);
    }

    internal void PlayParticles(Color color)
    {
        for (int i = 0; i < mouthParticle.Count; i++)
        {
            
            SetParticleSystemColor(mouthParticle[i],color);
            ParticleSystem[] childParticles = mouthParticle[i].GetComponentsInChildren<ParticleSystem>();
            for (int j = 0; j < childParticles.Length; j++)
            {
                SetParticleSystemColor(childParticles[j], color);
            }
        }

        mouthParticle[0].Play();
    }


    //Particle Color
    private void SetParticleSystemColor(ParticleSystem particleSystem, Color color)
    {
        var main = particleSystem.main;
        main.startColor = color;
    }

    

    
}
