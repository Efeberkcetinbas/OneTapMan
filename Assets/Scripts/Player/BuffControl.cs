using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffControl : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private List<ParticleSystem> buffParticles;
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnInfinity,OnInfinity);
        EventManager.AddHandler(GameEvent.OnPassionVictory,OnPassionVictory);
        EventManager.AddHandler(GameEvent.OnOnePunchMan,OnOnePunchMan);
        EventManager.AddHandler(GameEvent.OnCloseInfinity,OnCloseInfinity);
        EventManager.AddHandler(GameEvent.OnClosePassionVictory,OnClosePassionVictory);
        EventManager.AddHandler(GameEvent.OnCloseOnePunchMan,OnCloseOnePunchMan);

    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnInfinity,OnInfinity);
        EventManager.RemoveHandler(GameEvent.OnPassionVictory,OnPassionVictory);
        EventManager.RemoveHandler(GameEvent.OnOnePunchMan,OnOnePunchMan);
        EventManager.RemoveHandler(GameEvent.OnCloseInfinity,OnCloseInfinity);
        EventManager.RemoveHandler(GameEvent.OnClosePassionVictory,OnClosePassionVictory);
        EventManager.RemoveHandler(GameEvent.OnCloseOnePunchMan,OnCloseOnePunchMan);

    }


    private void OnInfinity()
    {
        //Buff Time kadar suruyor. MoveNumber eksilmiyor
        playerData.isInfinity=true;
        OpenParticle(0);
    }

    private void OnPassionVictory()
    {
        //Buff Time kadar suruyor. Her vurdugumuzda +3 Move Number Ekleniyor
        playerData.isPassionVictory=true;
        OpenParticle(1);
    }


    private void OnOnePunchMan()
    {
        //Buff Time kadar suruyor. 100'u vurursa oyuncu butun yapilar yok oluyor
        playerData.isOnePunchMan=true;
        OpenParticle(2);
    }

    private void OnCloseInfinity()
    {
        playerData.isInfinity=false;
        CloseParticles();
    }

    private void OnClosePassionVictory()
    {
        playerData.isPassionVictory=false;
        CloseParticles();
    }

    private void OnCloseOnePunchMan()
    {
        playerData.isOnePunchMan=false;
        CloseParticles();
    }

    //Hand uzerinde buff particle
    private void OpenParticle(int id)
    {
        buffParticles[id].gameObject.SetActive(true);
        buffParticles[id].Play();
    }
    private void CloseParticles()
    {
        for (int i = 0; i < buffParticles.Count; i++)
        {
            buffParticles[i].Stop();
            buffParticles[i].gameObject.SetActive(false);
        }
    }
}
