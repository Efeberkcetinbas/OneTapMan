using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DestroyTypes
{
    Magic,
    BlackHole,
    Gojo,
    Sukuna
}
public class DestroyNumber : MonoBehaviour
{   
    [SerializeField] private LevelData levelData;
    internal void MakeDestruction()
    {
        switch(levelData.destroyTypes)
        {
            case DestroyTypes.Magic:
                //EventManager.Broadcast(GameEvent.OnMagicDestroy)
                MagicDestruction();
                break;
            case DestroyTypes.BlackHole:
                BlackHoleDestruction();
                break;
            case DestroyTypes.Gojo:
                GojoDestruction();
                break;
            case DestroyTypes.Sukuna:
                SukunaDestruction();
                break;

        }
    }

    private void MagicDestruction()
    {
        Debug.Log("MAGIC");
        Destruction();
    }

    private void BlackHoleDestruction()
    {
        Debug.Log("BLACKHOLE");
        Destruction();
    }

    private void GojoDestruction()
    {
        Debug.Log("GOJO");
        Destruction();
    }

    private void SukunaDestruction()
    {
        Debug.Log("SUKUNA");
        Destruction();
    }

    private void Destruction()
    {
        EventManager.Broadcast(GameEvent.OnIncreaseScore);
        gameObject.SetActive(false);
    }
    
}
