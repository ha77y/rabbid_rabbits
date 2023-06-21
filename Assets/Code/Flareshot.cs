using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flareshot : MonoBehaviour
{
    public Character player;
    public float Thrust = 20000f;
    public GameObject Flare;
    private GameObject currentflare;
    Rigidbody rb;
    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(player.numFlare >= 1)
            {
                rotation.eulerAngles = new Vector3(player.lookDirection.y, player.lookDirection.x, player.lookDirection.z);
                currentflare = Instantiate(Flare,player.transform.position+player.transform.forward * 1,rotation);
                rb = currentflare.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * Thrust,ForceMode.Impulse);
            }
        }
    }
}
