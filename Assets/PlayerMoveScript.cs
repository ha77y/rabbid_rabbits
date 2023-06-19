using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public Transform playerT;
    private const float floatToRad = Mathf.PI / 180;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerT.localPosition = new Vector3(
            playerT.position.x  + (Input.GetAxis("Horizontal")*Time.deltaTime),                                             //x
            playerT.position.y + Input.GetAxis("Vertical") * Time.deltaTime * Mathf.Sin((playerT.rotation.x / floatToRad)*2 ),   //y
            playerT.position.z + Input.GetAxis("Vertical") * Time.deltaTime * Mathf.Cos((playerT.rotation.x / floatToRad)*2 ));  //z

        playerT.rotation =Quaternion.Euler(playerT.rotation.eulerAngles.x + Input.GetAxis("Mouse Y")*-1, playerT.rotation.eulerAngles.y + Input.GetAxis("Mouse X"), playerT.rotation.eulerAngles.z);

        print(playerT.rotation.x * floatToRad);

        //Mathf.Abs
    }
}
