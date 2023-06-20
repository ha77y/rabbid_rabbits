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

        //Cursor Lock
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKey(KeyCode.Space))
            Cursor.lockState = CursorLockMode.None;  
        
        movement();

        interactionCheck();






       


       


    }



    private void movement()
    {
        //Rotation
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * -1 * cameraSens, transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * cameraSens, transform.rotation.eulerAngles.z);
        //Movement
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Input.GetAxisRaw("Vertical") * moveSpeed);
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * Input.GetAxisRaw("Horizontal") * moveSpeed);
    }


    private void interactionCheck()
    {

    }
}
