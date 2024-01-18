using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> breaks=new List<Rigidbody>();

    private WaitForSeconds waitForSeconds;


    private void Start()
    {
        waitForSeconds=new WaitForSeconds(2);
    }


    // !!!!!!!!!!!!!!!!!!!!!!!!!!  Obstacle da olabilir duruma gore bak 
    internal void OnHitSword()
    {
        for (int i = 0; i < breaks.Count; i++)
        {
            breaks[i].isKinematic=false;
        }
    }


    
}
