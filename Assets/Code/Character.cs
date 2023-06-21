using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;




public class MovementState
{

    public virtual void movement(Character player) { }
    public virtual void switchMovement(Character player) { }
    public virtual void initialize(Character player) { }
}


public class SwimState : MovementState
{
    public override void movement(Character player)
    {


        //Rotation
        player.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * -1 * player.cameraSens, player.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * player.cameraSens, player.transform.rotation.eulerAngles.z);
        //Movement
        player.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Input.GetAxisRaw("Vertical") * player.moveSpeed);
        player.GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * Input.GetAxisRaw("Horizontal") * player.moveSpeed);

    }

    public override void switchMovement(Character player)
    {

            player.movementState = new WalkState();
            player.movementState.initialize(player);
            Debug.Log("WalkState");

    }

    public override void initialize(Character player)
    {
        player.moveSpeed = 2f;
        player.GetComponent<Rigidbody>().drag = 3;
        player.GetComponent<Rigidbody>().useGravity = false ;
        player.GetComponent<CapsuleCollider>().enabled = false;
        //change character model orientation
    }
}

public class WalkState : MovementState
{

    public override void movement(Character player)
    {

        //Rotation
        player.transform.rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * player.cameraSens, 0);
        player.GetComponentInChildren<Camera>().transform.localRotation = Quaternion.Euler(player.GetComponentInChildren<Camera>().transform.localRotation.eulerAngles.x + Input.GetAxis("Mouse Y") * -1 * player.cameraSens, 0, 0);

        //Movement
        player.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Input.GetAxisRaw("Vertical") * player.moveSpeed);
        player.GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * Input.GetAxisRaw("Horizontal") * player.moveSpeed);

    }

    public override void switchMovement(Character player)
    {

         player.movementState = new SwimState();
         player.movementState.initialize(player);
         Debug.Log("SwimState");

    }

    public override void initialize(Character player)
    {
        player.moveSpeed = 4f;
        player.GetComponent<Rigidbody>().drag = 5;
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<CapsuleCollider>().enabled = true;
    }
}

public class AnimState : MovementState
{

    public override void movement(Character player)
    {

    }

    public override void switchMovement(Character player)
    {

    }

    public override void initialize(Character player)
    {

    }
}

public class Character : MonoBehaviour
{


    public MovementState movementState;
    public float moveSpeed = 2f;
    public float cameraSens = 2f;
    public CapsuleCollider walkCollider;

    public int numFlare = 0;
    public int numPatch = 0;
    public int health = 100;
    public float oxygen = 100;

    public int maxItems = 3;


    public Vector2 lookDirection;
    // Start is called before the first frame update
    void Start()
    {
        movementState = new SwimState();
        movementState.initialize(this);
    }

    // Update is called once per frame
    void Update()
    {

        //Cursor Lock
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKey(KeyCode.Space))
            Cursor.lockState = CursorLockMode.None;

        movementState.movement(this);


        if (Input.GetKeyDown("e"))
        {
            interactionCheck();
        }

        if (movementState is SwimState)
        {
            oxygen -= Time.deltaTime;
        }

        setLookDir();
    }

    private void interactionCheck()
    {

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance))
        {
            //if the interact is a cube interact
            if (raycastHit.transform.parent.TryGetComponent(out InteractableObject objectInteract))
            {
                objectInteract.Interact(this);
            }
        }
    }

    public void setLookDir()
    {
        if(movementState is SwimState)
        {
            lookDirection.y = transform.eulerAngles.x + 270;

        }
        else
        {
            lookDirection.y = transform.GetComponentInChildren<Camera>().transform.eulerAngles.x + 270;
        }
        lookDirection.x = transform.eulerAngles.y;
            if(lookDirection.y >= 360)
            {
                lookDirection.y -= 360;
            }
            lookDirection.y -= 180;
            lookDirection.y = 180 - lookDirection.y;
            //Debug.Log(lookDirection);
    }

    public void doorMove(Door door)
    {
        bool enter = false;
        if (movementState is SwimState) {   enter = true;   }
        movementState = new AnimState();
        float animDur = 1f;
        StartCoroutine(MoveToDoor(animDur, this, door, enter));
    }

    IEnumerator MoveToDoor(float duration, Character player, Door door, bool enter)
    {
        float time = 0;
        float entDist = 3f;
        if (!enter)
        {
            entDist = -1.5f;
        }

        Vector3 targetPosition = door.transform.position + door.transform.forward * entDist;

        Vector3 startPosition = player.transform.position;
        while (time < duration)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            player.transform.LookAt(door.transform);
            yield return null;
        }
        player.transform.position = targetPosition;

        door.open(player, enter);
    }

    public void doorEnter(Character player, Door door, bool enter)
    {

        StartCoroutine(Enter(player, door, enter));
    }

    IEnumerator Enter(Character player, Door door, bool enter)
    {
        float duration = 1.5f;
        Debug.Log("Entering");
        float time = 0;
        float entDist = -1.5f;

        bool closed = false;
        if (!enter)
        {
            ///EXIT DISTANCE
            entDist = 4.5f;
        }

        Vector3 targetPosition = door.transform.position + door.transform.forward * entDist;

        Vector3 startPosition = player.transform.position;
        while (time < duration)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            //player.transform.LookAt(door.transform);

            if (time >= 0.75 && closed == false)
            {
                door.close();
                closed = true;
            }
            yield return null;
        }
        player.transform.position = targetPosition;



        // should prolly put this in the animstate for consistency
        if (enter)
        {
            player.movementState = new WalkState();
        }
        else
        {
            player.movementState = new SwimState();
        }


    }


}
