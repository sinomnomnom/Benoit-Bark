using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject pivotPoint;
    public float speed = 1;
    private bool locked = false;

    private Vector2 LastMousePos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (locked) return;
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) 
        {
            LastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            Vector2 currentMousePos = Input.mousePosition;
            if (LastMousePos.x != currentMousePos.x)
            {
                pivotPoint.transform.RotateAround(pivotPoint.transform.position, Vector3.up, (currentMousePos.x - LastMousePos.x) * speed);
            }
            LastMousePos = currentMousePos;
        }
    }
}
