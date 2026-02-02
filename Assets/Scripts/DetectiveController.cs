using System.Collections.Generic;
using UnityEngine;

public class DetectiveController : MonoBehaviour
{
    public Animator animator;
    public Collider collider;
    public AudioClip whoosh;
    public AudioSource audioSource;
    Transform cam;
    
    List<Collider> TriggerList = new List<Collider>();

    public bool active = false;
    public bool interacting = false;
    public Vector3 direction = Vector3.zero;
    public float speed;
    private bool left;
    private bool moving;

    private Vector3 velocity;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    public void Update()
    {
        if (active && !interacting)
        {
            Move();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Interact();
                print("interact!");
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchPerspectives();
            }
        }
        else
        {
            
        }

    }

    public void Move()
    {
        direction = GetDirection();
        Vector3 localDirection = transform.InverseTransformVector(direction);

        if (localDirection.x > 0 && left)
        {
            left = false;
            
        }
        if (localDirection.x < 0 && !left)
        {
            left = true;
            
        }
        if (localDirection == Vector3.zero)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }

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
    public void SwitchPerspectives()
    {
        audioSource.PlayOneShot(whoosh, 0.5f);
        Services.GameController.SwitchActiveCharacter();
        active = false;
    }
    public void Interact()
    {
        Collider closestCol = collider;

        float dist = 10000f;
        foreach (Collider col in TriggerList)
        {
            print("interacting");
            if (col.gameObject.tag != "NPC" &&
                col.gameObject.tag != "Dog" &&
                col.gameObject.tag != "Item" &&
                col.gameObject.tag != "Detective")
            {
                continue;
            }
            float testDist = Vector3.Distance(col.gameObject.transform.position, transform.position);
            if (testDist < dist)
            {
                closestCol = col;
                dist = testDist;
            }
        }
        if (closestCol != collider)
        {
            switch (closestCol.gameObject.tag)
            {
                case "NPC":
                    print("npc interaction");
                    NPCController npc = closestCol.gameObject.GetComponent<NPCController>();
                    string node = npc.detectiveDialogueNode;
                    print(node);
                    Services.DialogueRunner.StartDialogue(node);
                    interacting = true;
                    Services.DialogueRunner.onDialogueComplete.AddListener(() => { interacting = false; });
                    break;
                case "Item":
                    print("Item interaction: " + closestCol.gameObject.name);
                    ItemController item = closestCol.gameObject.GetComponent<ItemController>();

                    if (item.door)
                    {
                        if (!item.locked)
                        {
                            transform.position = item.newPlayerPos;
                            Services.GameController.dog.transform.position = item.newIncativePlayerPos;
                            Services.GameController.Camera.transform.position = item.newCameraPos;
                            break;
                        }
                        if (item.locked)
                        {
                            interacting = true;
                            Services.DialogueRunner.StartDialogue("Locked");
                            Services.DialogueRunner.onDialogueComplete.AddListener(() => { interacting = false; });
                            break;
                        }
                    }
                    break;
                case "Dog":
                    interacting = true;
                    Services.DialogueRunner.StartDialogue("DetectiveToDog");
                    Services.DialogueRunner.onDialogueComplete.AddListener(() => { interacting = false; Services.GameController.SetTheme(ScentDatabase.Scents.NONE); });
                    break;
                default:
                    print("untagged item: " + closestCol.gameObject.name);
                    break;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!TriggerList.Contains(other))
        {
            TriggerList.Add(other);
            print("new collider entered!");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (TriggerList.Contains(other))
        {
            TriggerList.Remove(other);
            print("collider exited!");
        }
    }
}
