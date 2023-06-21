using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flareshot : MonoBehaviour
{
    public Character player;
    public Rigidbody RB;
    public float Thrust = 20f;
    public GameObject Flare;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(""))
        {
            if(player.numFlare <= 1)
            {
                instantiate
            }
        }
    }
}
