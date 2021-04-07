using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    void Update()
    {
        var camTransform = transform;
        var positionToFollow = _playerTransform.position;
        
        camTransform.position =
            new Vector3(positionToFollow.x, positionToFollow.y, camTransform.position.z);
    }
}
