using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float MoveSpeed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * -1, transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X"), transform.rotation.eulerAngles.z);

        //Movement
        transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * MoveSpeed);
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * MoveSpeed);

    }
}
