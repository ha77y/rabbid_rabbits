using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor Lock
        if (Input.GetMouseButtonDown(1))
            Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKey(KeyCode.Space))
            Cursor.lockState = CursorLockMode.None;
    }
}
