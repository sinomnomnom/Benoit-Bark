using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DogPuppet : MonoBehaviour
{
    public Animator animator;
  
    public Vector2 direction = Vector2.zero;
    public float speed;
    private bool left;
    private bool moving;
  

    private Vector2 velocity;

    public void Start()
    {

    }

    public void Update()
    {
        Move();      
    }

    public void Move()
    {
        direction = GetDirection();
       
        if (direction.x > 0 && left)
        {
            left = false;
            animator.SetBool("left", left);
        }
        if (direction.x < 0 && !left)
        {
            left = true;
            animator.SetBool("left", left);
        }
        if (direction == Vector2.zero)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }

        animator.SetBool("moving", moving);

        velocity += direction * speed;
        velocity = Vector2.ClampMagnitude(velocity, speed);

        transform.position += new Vector3(velocity.x, 0, velocity.y) * Time.deltaTime;
        velocity *= .95f;
    }

    private Vector2 GetDirection()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.A)) { direction.x -= 1; }
        if (Input.GetKey(KeyCode.D)) { direction.x += 1; }
        if (Input.GetKey(KeyCode.S)) { direction.y -= 1; }
        if (Input.GetKey(KeyCode.W)) { direction.y += 1; }

        return direction;
    }
}
