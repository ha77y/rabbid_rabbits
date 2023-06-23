using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



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
            //Debug.Log("WalkState");

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
         //Debug.Log("SwimState");

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
    public int maxSamples = 4;
    public bool hasKeycard = false;

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

    public GameObject cursor;
    // Start is called before the first frame update
    void Start()
    {
        movementState = new SwimState();
        movementState.initialize(this);
    }

    // Update is called once per frame
    void Update()
    {
        //interactHighlight();
        if (iFrame > 0)
        {
            iFrame -= Time.deltaTime;
        }


        if (Input.GetMouseButtonDown(0))
            knife();

            //Cursor Lock
            if (Input.GetMouseButtonDown(1))
            Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKey(KeyCode.Space))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetKeyDown(KeyCode.F))
        {
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf); //Toggle Flashlight
        }

        if (Input.GetKey(KeyCode.LeftShift)&& movementState is SwimState)
        {
            transform.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * moveSpeed);
        }
        if (Input.GetKey(KeyCode.LeftControl) && movementState is SwimState)
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

        if(samples >= maxSamples)
        {
            win();
        }

        if(health <=0 || oxygen <= 0)
        {
            lose();
        }

    }

    private void interactionCheck()
    {

        float interactDistance = 5f;


        Vector3 forwardDir;
        if(movementState is WalkState)
        {
            forwardDir = this.GetComponentInChildren<Camera>().transform.forward;
        }
        else
        {
            forwardDir = transform.forward;
        }
        if (Physics.Raycast(transform.position, forwardDir, out RaycastHit raycastHit, interactDistance))
        {
            //Debug.Log(raycastHit);

            //if the parent is an interactable
            if (raycastHit.transform.parent.TryGetComponent(out InteractableObject objectInteract))
            {

                //Debug.Log("interact");
                objectInteract.Interact(this);
            }
            else
            {

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

        Transform first;
        Transform second;

        if (enter)
        {
            first = door.transform.GetChild(2);
            second = door.transform.GetChild(3);
        }
        else
        {
            first = door.transform.GetChild(3);
            second = door.transform.GetChild(2);
        }

        Vector3 targetPosition = first.position;
        Vector3 startPosition = player.transform.position;

        float time = 0;
        float duration = 1f;
        while (time < duration)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            player.transform.LookAt(second);
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


        duration = 1.5f;
        time = 0;

        targetPosition = second.position;
        startPosition = player.transform.position;

        while (time < duration)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        player.transform.position = targetPosition;

        transform.GetComponentInChildren<Camera>().transform.rotation = player.transform.rotation; 
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
            iFrame = 0.2f;
        }
    }


    public void win()
    {
        Debug.Log("Win");
        //Application.Quit();
    }
    public void lose()
    {
        //Application.Quit();
    }

    public void knife()
    {
       // Debug.Log("Knife");
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, 2f)){
            //Debug.Log(raycastHit.transform);
            if (raycastHit.transform.TryGetComponent(out Monster monster))
            {
                //Debug.Log("MonsterHit");
                monster.takeDamage();
            }
        }

    }


    //public void interactHighlight()
    //{
    //    float interactDistance = 3f;
    //    cursor.GetComponent<Image>().color = Color.white;
    //    if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance))
    //    {


    //        if (raycastHit.transform.parent)
    //        {
    //        if (raycastHit.transform.TryGetComponent(out InteractableObject objectInteract))
    //        {
    //            cursor.GetComponent<Image>().color = Color.blue ;
    //        }
    //        }

    //    }
    //}

}
