using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerTypes
{
    Slime1,
    Slime1King,
    Slime1MetalHelmet,
    Slime2,
    Slime3King,
    Slime3Leaf,
    Slime3Sprout,
    Slime3
}

public class PlayerMesh : MonoBehaviour
{
    [SerializeField] private List<Transform> characters=new List<Transform>();

    [SerializeField] private PlayerTypes playerTypes;

    public Transform Mouth;


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
        Debug.Log("MESH,PARTICLE");
    }

    

    
}
