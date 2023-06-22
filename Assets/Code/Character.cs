using System.Collections;
using System.Collections.Generic;
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
    public int samples = 0;


    public MovementState movementState;
    public float moveSpeed = 2f;
    public float cameraSens = 2f;
    public CapsuleCollider walkCollider;

    public int numFlare = 0;
    public int numPatch = 0;
    public int health = 100;
    public float oxygen = 100;

    public int maxItems = 3;

    public float iFrame = 0f;
    public Vector3 lookDirection;
    // Start is called before the first frame update
    void Start()
    {
        movementState = new SwimState();
        movementState.initialize(this);
    }

    // Update is called once per frame
    void Update()
    {

        if(iFrame > 0)
        {
            iFrame -= Time.deltaTime;
        }

        //Cursor Lock
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKey(KeyCode.Space))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetKeyDown(KeyCode.F))
        {
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf); //Toggle Flashlight
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * moveSpeed);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * -moveSpeed);
        }

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
            Debug.Log(raycastHit);

            //if the interact is a door interact
            if (raycastHit.transform.parent.TryGetComponent(out InteractableObject objectInteract))
            {

                Debug.Log("interact");
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
        StartCoroutine(UseDoor(this, door, enter));
        door.pass(this, enter);
    }

    IEnumerator UseDoor(Character player, Door door, bool enter)
    {

        float entDist = 3f;
        if (!enter)
        {
            entDist = -1.5f;
        }

        Vector3 targetPosition = door.transform.position + door.transform.forward * entDist;
        Vector3 startPosition = player.transform.position;

        float time = 0;
        float duration = 1f;
        while (time < duration)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            player.transform.LookAt(door.transform);
            yield return null;
        }
        player.transform.position = targetPosition;

        time = 0;
        duration = 1.5f;
        while (time < duration)
        {
            //delay
            time += Time.deltaTime;
            yield return null;
        }


        entDist = -1.5f;
        if (!enter)
        {
            ///EXIT DISTANCE
            entDist = 4.5f;
        }
        duration = 1.5f;
        time = 0;

        targetPosition = door.transform.position + door.transform.forward * entDist;
        startPosition = player.transform.position;

        while (time < duration)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
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

    public void takeDamage()
    {
        if(iFrame <= 0)
        {
            health -= 5;
            iFrame = 1;
        }
    }


}
