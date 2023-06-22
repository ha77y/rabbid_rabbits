using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flareshot : MonoBehaviour
{
    public Character player;
    public float Thrust = 20000f;
    public GameObject Flare;
    public GameObject currentflare;
    Rigidbody rb;
    Quaternion rotation;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(player.numFlare >= 1)
            {
                rotation.eulerAngles = new Vector3(player.lookDirection.y, player.lookDirection.x, player.lookDirection.z);
                Destroy(currentflare);
                currentflare = Instantiate(Flare,player.transform.position+player.transform.forward * 1,rotation);
                rb = currentflare.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * Thrust,ForceMode.Impulse);
                player.numFlare--;
                timer = 10; ////RESET LATER
            }
        }
        if (timer < 0)
        {
            Destroy(currentflare);
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}
