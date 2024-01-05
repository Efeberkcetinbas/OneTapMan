using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum DestroyTypes
{
    OnePunch,
    Magic,
    BlackHole,
    Gojo,
    Sukuna
}
public class DestroyNumber : MonoBehaviour
{   
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameObject destructionObject;
    [SerializeField] private Ease ease;
    internal void MakeDestruction()
    {
        switch(levelData.destroyTypes)
        {
            case DestroyTypes.OnePunch:
                //EventManager.Broadcast(GameEvent.OnMagicDestroy)
                //PunchDestruction();
                break;

            case DestroyTypes.Magic:
                //EventManager.Broadcast(GameEvent.OnMagicDestroy)
                //MagicDestruction();
                break;
            case DestroyTypes.BlackHole:
                //BlackHoleDestruction();
                break;
            case DestroyTypes.Gojo:
                //GojoDestruction();
                break;
            case DestroyTypes.Sukuna:
                //SukunaDestruction();
                break;

        }
    }

    private void PunchDestruction()
    {
        Debug.Log("PUNCH");
        Destruction();
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

    internal void CreateDestructionObject(Transform fromPos,Transform targetPos)
    {
        //Bu ileride yumruk degil baska sekillerde olacak
        GameObject destructionClone=Instantiate(destructionObject);
        destructionClone.transform.position=fromPos.position;
        destructionClone.transform.DOMove(targetPos.position,0.25f).SetEase(ease).OnComplete(()=>{
            Destroy(destructionClone);
            Destruction();
            //Collider ile carpisma gerceklesir
            
        });
    }
    
}
