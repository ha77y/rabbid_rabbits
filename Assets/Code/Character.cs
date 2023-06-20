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
        //change character model orientation
    }
}

public class WalkState : MovementState
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

            player.movementState = new SwimState();
            player.movementState.initialize(player);
            Debug.Log("SwimState");


    }

    public override void initialize(Character player)
    {

    }
}



public class Character : MonoBehaviour
{


    public MovementState movementState;

    public float moveSpeed = 1f;

    public float cameraSens = 1f;


    public Vector3 test;

    // Start is called before the first frame update
    void Start()
    {
        movementState = new SwimState();
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


    }



    private void movement()
    {
        ////Rotation
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * -1 * cameraSens, transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * cameraSens, transform.rotation.eulerAngles.z);
        ////Movement
        //GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Input.GetAxisRaw("Vertical") * moveSpeed);
        //GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * Input.GetAxisRaw("Horizontal") * moveSpeed);
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
}
