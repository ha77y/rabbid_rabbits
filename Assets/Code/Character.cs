using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed = 1f;


    public float cameraSens = 1.5f;

    public Vector3 test;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * -1 * cameraSens, transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * cameraSens, transform.rotation.eulerAngles.z);

        movementCheck();
        //Cursor Lock

        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKey(KeyCode.Space))
            Cursor.lockState = CursorLockMode.None;



        interactionCheck();


        //Rotation
       

        //Movement
        //transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxisRaw("Vertical") * moveSpeed);
        //transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxisRaw("Horizontal") * moveSpeed);

        //GetComponent<Rigidbody>().MovePosition(transform.position + Vector3.forward * Time.deltaTime * Input.GetAxisRaw("Vertical") * moveSpeed);
        //GetComponent<Rigidbody>().MovePosition(transform.position + Vector3.right * Time.deltaTime * Input.GetAxisRaw("Horizontal") * moveSpeed);

        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Input.GetAxisRaw("Vertical") * moveSpeed);
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * Input.GetAxisRaw("Horizontal") * moveSpeed);


    }



    private void movementCheck()
    {

        RaycastHit hit;
        Vector2 inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputMovement = inputMovement.normalized;
        Vector3 characterMovement = new Vector3(inputMovement.x, 0f, inputMovement.y);

        float moveDistance = Time.deltaTime * moveSpeed;
        float height = 2f;
        float radius = 0.5f;

        //bool collision = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * height, radius, characterMovement, moveDistance);

        test = transform.forward;


        //var rot = Quaternion.Euler((inputMovement.x, 0f, inputMovement.y);


        Vector3 rearrangeMovement = new Vector3(characterMovement.x, characterMovement.y, 0);
        Vector3 movementDir = Vector3.Scale(transform.forward, characterMovement);


        bool collision = Physics.SphereCast(transform.position, radius, movementDir , out hit, moveDistance);




        //diagonal movement exception check

        /*
        if (collision)
        {
            //check if chacter can move on only x
            Vector3 characterMovementX = new Vector3(characterMovement.x, 0f, 0f);
            collision = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * height, radius, characterMovementX, moveDistance);
            if (!collision)
            {
                characterMovement = characterMovementX;
            }
            else
            {
                //Cant move on only x check if can move on only z
                Vector3 characterMovementZ = new Vector3(0f, 0f, characterMovement.z);
                collision = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * height, radius, characterMovementZ, moveDistance);
                if (!collision)
                {
                    characterMovement = characterMovementZ;
                }
            }
        }
        

        if (!collision) {
            transform.Translate(Vector3.forward * Time.deltaTime * characterMovement.z);
            transform.Translate(Vector3.right * Time.deltaTime * characterMovement.x);
        }

        */
    }


    private void interactionCheck()
    {

    }
}
