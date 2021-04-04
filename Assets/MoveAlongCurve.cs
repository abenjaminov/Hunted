using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongCurve : MonoBehaviour
{
    public float Speed = 3;
    
    // Update is called once per frame
    void Update()
    {
        var nextY = transform.position.y + (Speed * Time.deltaTime);
        var nextX = Mathf.Exp(nextY);

        transform.position = new Vector3(nextX, nextY, 0);
    }
}
