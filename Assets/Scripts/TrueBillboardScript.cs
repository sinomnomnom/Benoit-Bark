using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueBillboardScript : MonoBehaviour
{
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(mainCamera.transform, Vector3.up);
        transform.Rotate(0, 180, 0);

    }
}
