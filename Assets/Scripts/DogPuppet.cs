using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DogPuppet : MonoBehaviour
{
    public Animator animator;
  
    public Vector3 direction = Vector3.zero;
    public float speed;
    private bool left;
    private bool moving;
  

    private Vector3 velocity;
    Transform cam;
    

    private void Start()
    {
        cam = Camera.main.transform;
    }

    public void Update()
    {
        Move();      
    }

    public void Move()
    {
        direction = GetDirection();
        Vector3 localDirection = transform.InverseTransformVector(direction);
        
        if (localDirection.x > 0 && left)
        {
            left = false;
            animator.SetBool("left", left);
        }
        if (localDirection.x < 0 && !left)
        {
            left = true;
            animator.SetBool("left", left);
        }
        if (localDirection == Vector3.zero)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }

        animator.SetBool("moving", moving);

        velocity += direction * speed;
        velocity = Vector3.ClampMagnitude(velocity, speed);

        transform.Translate(velocity * speed * Time.deltaTime, Space.World);


        velocity *= .9f;
    }

    private Vector3 GetDirection()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.A)) { direction.x -= 1; }
        if (Input.GetKey(KeyCode.D)) { direction.x += 1; }
        if (Input.GetKey(KeyCode.S)) { direction.y -= 1; }
        if (Input.GetKey(KeyCode.W)) { direction.y += 1; }

        Vector3 camPosition = new Vector3(cam.position.x, transform.position.y, cam.position.z);
        Vector3 camdirection = (transform.position - camPosition).normalized;

        Vector3 forwardMovement = camdirection * direction.y;
        Vector3 horizontalMovement = cam.right * direction.x;

        Vector3 direction3d = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1);
        return direction3d;
    }
}
