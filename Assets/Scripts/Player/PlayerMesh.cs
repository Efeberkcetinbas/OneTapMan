using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMesh : MonoBehaviour
{
    [SerializeField] private List<Transform> characters=new List<Transform>();

    [SerializeField] private PlayerData playerData;
    public Transform Mouth;

    [SerializeField] private ParticleSystem destroyParticle;

    


    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnFail,OnFail);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);

    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnFail,OnFail);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
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


    private void OnRestartLevel()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].gameObject.SetActive(false);
        }

        characters[playerData.selectedCharacterIndex].gameObject.SetActive(true);
    }
    

    
}
