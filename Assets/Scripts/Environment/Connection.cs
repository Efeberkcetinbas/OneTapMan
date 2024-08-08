using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
    private LineRenderer line;

    [SerializeField] private Transform line1,line2;

    void Start()
    {
        line=GetComponent<LineRenderer>();
        line.positionCount=2;
    }
    void Update()
    {
        line.SetPosition(0,line1.position);
        line.SetPosition(1,line2.position);
    }
}
