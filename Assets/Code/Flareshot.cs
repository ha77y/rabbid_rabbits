using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flareshot : MonoBehaviour
{
    public Character player;
    public float Thrust = 20f;
    public GameObject Flare;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(player.numFlare <= 1)
            {
                Instantiate(Flare);
                rb = Flare.GetComponent<Rigidbody>();
                rb.AddForce(player.transform.forward * Thrust);
            }
        }
    }
}
