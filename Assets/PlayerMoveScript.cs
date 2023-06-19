using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public Transform playerT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerT.position = new Vector3(playerT.position.x + Input.GetAxis("Horizontal"), playerT.position.y, playerT.position.z + Input.GetAxis("Vertical"));

    }
}
