using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public async Task SpinCameraAround(Vector3 pos)
    {
        if (locked) return;
        locked = true;
        Vector3 initialPos = pivotPoint.transform.position;
        /*
        while (Vector3.Distance(pivotPoint.transform.position, pos) > .5)
        {
            pivotPoint.transform.Translate((pos - pivotPoint.transform.position * Time.deltaTime), Space.World);
            await Task.Yield();
        }
        */
        
        float rotations = 0;
        float speed = 0f;
        while (rotations < 2.5*360)
        {
            speed += (300-speed)*Time.deltaTime;
            float rotationDistance = speed * Time.deltaTime * 2;
            pivotPoint.transform.RotateAround(pivotPoint.transform.position, Vector3.up,rotationDistance);
            rotations += rotationDistance;
            await Task.Yield();
        }
        rotations = 0;
        while (speed > 0)
        {
            speed += (-1.5f-speed) * Time.deltaTime;
            float rotationDistance = speed * Time.deltaTime * 2;
            pivotPoint.transform.RotateAround(pivotPoint.transform.position, Vector3.up, rotationDistance);
            rotations += rotationDistance;
            await Task.Yield();
        }

        /*
        while (Vector3.Distance(pivotPoint.transform.position, pos) > .5)
        {
            pivotPoint.transform.Translate(( pivotPoint.transform.position-pos) * Time.deltaTime, Space.World);
            await Task.Yield();
        }
        */
        LastMousePos = Input.mousePosition;
        locked = false;
    }
}
