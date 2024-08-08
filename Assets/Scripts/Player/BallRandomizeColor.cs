using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRandomizeColor : MonoBehaviour
{
    [SerializeField] private List<Color> colors=new List<Color>();

    private Material material;

    

    void OnEnable()
    {
        material=GetComponent<MeshRenderer>().material;
        material.color = colors[Random.Range(0, colors.Count)];
    }
}
