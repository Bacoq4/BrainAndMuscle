using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverAtTwoPoint : MonoBehaviour
{
    public Transform Pos1;
    public Transform Pos2;
    Transform target;
    public float speed = 1f;

    void Start()
    {
        target = Pos1;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        
        if (Vector3.Distance(transform.position,Pos1.position) < 0.1f)
        {
            target = Pos2;
        }
        else if (Vector3.Distance(transform.position, Pos2.position) < 0.1f)
        {
            target = Pos1;
        }
    }
}






