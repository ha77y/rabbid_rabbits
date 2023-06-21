using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sonar : MonoBehaviour
{

    public Character player;

    public float rotation = 0;

    public Image sonarBase;

    public Image sonarSweep;
    public Image sonarDirection;
    // Start is called before the first frame update

    public float sonarRadius = 100f;

    public RaycastHit[] Hits;
    public Vector3 boxSize;


    public Quaternion orientation;
    public Vector3 direction;

    public float angle = 0;
    void Start()
    {
        boxSize = new Vector3(0.1f, 400, 0.1f) ;
    }


    void Update()
    {

        rotation = player.transform.eulerAngles.y;
        sonarDirection.transform.rotation = Quaternion.Euler(0, 0, -rotation);
        sonarSweep.transform.Rotate(0, 0, 1);


        //direction = player.transform.forward;
        //direction.y = 0;
        if (angle >= 360)
        {
            angle = 0;

        }
        else
        {
            angle++;
        }

        direction = new Vector3(angle,0,0);

        Hits = Physics.BoxCastAll(player.transform.position, boxSize, new Vector3(0, angle, 0), orientation, sonarRadius);



        for (int i = 0; i < Hits.Length; i++)
        {
            if (Hits[i].transform.tag == "Sonar")
            {
                Debug.Log(Hits[i].transform);
            }

        }
    }
}
